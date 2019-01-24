<template>

  <div class="content">
    <div class="content-header">
      <h2>Semester Manage</h2>
    </div>

    <div class="box box-default" :class="{'collapsed-box': style.boxCollapse}">
      <div class="box-header with-border">
        <h3 class="box-title">{{  isEdit ? " Semester Edit" : "Semester Create"   }}</h3>
        <div class="box-tools pull-right">
          <!-- Buttons, labels, and many other things can be placed here! -->
          <!-- Here is a label for example -->
          <button class="btn btn-box-tool" @click="style.boxCollapse = !style.boxCollapse">
            <i :class="{'fa-plus': style.boxCollapse, 'fa-minus': !style.boxCollapse}" class="fa"></i>
          </button>
        </div>
        <!-- /.box-tools -->
      </div>
      <!-- /.box-header -->
      <div class="box-body">
        <form class="form-horizontal">
          <div class="form-group" >
            <label for="semester" class="col-sm-2 control-label">Semester Name</label>
            <div class="col-sm-10">
              <input type="text"
                     name="semester"
                     class="form-control w25"
                     id="semester" placeholder="Semester Name" :class="{'error': errors.has('semester')}"
                     v-model="semesterName" v-validate.disable="'required'">
            </div>
          </div>

          <div class="form-group">
            <label for="status" class="col-sm-2 control-label">Status</label>
            <div class="col-sm-10">
              <select class="form-control w25" id="status" v-model="status">
                <option value="1">Active</option>
                <option value="2">Pending</option>
              </select>
            </div>
          </div>

          <div class="form-group">
            <label for="category" class="col-sm-2 control-label">Marks Category</label>
            <div class="col-sm-10">
              <select class="form-control w25" id="category" v-model="semesterId" >
                <option v-for="semester in semesters"  :value="semester.id">{{semester.name}}</option>
              </select>
            </div>
          </div>

          <div v-if="newCategory">
            <div class="row">
              <div class="col-md-12 col-sm-offset-2 mb10">
                <button class="btn btn-secondary" v-on:click.prevent="addCat">Add <i class="fa fa-plus"></i> </button>
              </div>
            </div>
            <div class="row" style="margin-bottom: 10px;" v-for="cat in categories" :key="cat.id">
              <div class="col-md-3 col-sm-offset-2">
                <label>Category Name</label>
                <input type="text" class="form-control" v-model="cat.name" :name="'name'+cat.id" v-validate="'required'" :class="{'error': errors.has('name'+cat.id)}" />
              </div>
              <div class="col-md-3">
                <label>Marks</label>
                <input type="text" class="form-control" v-model="cat.mark" :name="'mark'+cat.id" v-validate="'required|max_value:100|min_value:1'" :class="{'error': errors.has('mark'+cat.id)}"/>
              </div>
              <div class="col-md-3">
                <button class="btn btn-danger  mt25 ml25i" v-on:click.prevent="deleteCat(cat.id)"><i class="fa fa-times"></i> </button>
              </div>
              
            </div>
          </div>

          <div class="form-group">
            <label for="studnets" class="col-sm-2 control-label">Student List</label>
            <div class="col-sm-10">
              <input type="file" class="form-control w25" id="studnets">
            </div>
          </div>

          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <button type="submit" class="btn btn-info" v-on:click.prevent="submit">Create</button>
              <button type="submit" class="btn btn-default" v-on:click.prevent="reset"  v-if="isEdit">Reset</button>
            </div>
          </div>


        </form>

      </div>
      <!-- /.box-body -->
      <div class="box-footer">
        The footer of the box
      </div>
    </div>
    <appconfirm v-if="style.confirm" @close="style.confirm = false" :config="confirmModal"></appconfirm>
    <!--<button @click="style.confirm = true">Click</button>-->
    <apptable :tableConfig="config">
    </apptable>

  </div>

</template>

<script>

  import table from '../HelperComponents/clientTable'
  import confirmModal from '../HelperComponents/confirmModal'
  import { util } from '../mixins/util'
  import { setTimeout } from 'core-js';

  export default {

    mixins: [util],
     created() {

       let loader = this.$loading.show({
         loader: 'spinner',
         color: '#0ACFE8'
       });
       this.$http.get("/api/semester/semesters").then(response => {

         // console.log(this.__arrayCopy([response.data]))
         this.semesters = this.__arrayCopy(response.data)
         this.config.data = response.data;

         this.semesters.unshift({ id: -1, name: "New" });

        // console.log("Semesters_Copy", this.semesters);
       })
         .catch(error => { })
         .then(response => {
           loader.hide();
         });
    },
    data() {

      return {
        style: {
          boxCollapse: true,
          confirm: false
        },

        isEdit: false,
        semesterId: -1,
        semesterName: '',
        status: 1,
        semesters: [],
        categories: [
          { name: '', mark: 0, id: 1 }
        ],
        config: {
          data: [],
          columns: ["name", "status"],
          actions: {
            Edit: {
              callBack: this.edit,
              cssClass: "btn-success btn-sm"
            },
            Delete: {
              callBack: this.del,
              cssClass: "btn-danger btn-sm"
            }
          },

          templates: {

            status: function (row, h) {

              return row.status == 1
                ? h('label', { 'class': 'label label-success' }, "Active")
                : row.status == 2
                  ? h('label', { 'class': 'label label-primary' }, "Pending")
                  : h('label', { 'class': 'label label-danger' }, "error");
            }
          }
        },

        confirmModal: {
          callBack: (root) => {

            setTimeout(() => {

              root.$emit('close')

            }, 2000)

          }
        }
      }
      },

    methods: {
      deleteCat(id) {

        var index = this.categories.findIndex(x => x.id === id);
        this.categories.splice(index, 1);

      },

      addCat() {

        if (this.categories.length === 0) {
          this.categories.push({ name: '', mark: 0, id: 1 });
          return;
        }

        var id = this.categories[this.categories.length - 1].id;

        this.categories.push({
          name: '',
          mark: 0,
          id: ++id
        });
      },

      reset() {

        this.isEdit = false;

        this.categories = [
          { name: '', mark: 0, id: 1 }
        ];
        this.semesterId = -1;
        this.status = 1;
        this.semesterName = '';
      },


      submit() {

        var sum = 0;

        this.categories.forEach(function (cat) {
          sum += parseInt(cat.mark);
        });

        if (sum !== 100 && this.semesterId === -1) {
          this.$toastr.e('Marks total must equal to 100');
          return;
        }

        this.$validator.validateAll().then(result => {

          if (result) {

            if (this.semesterId !== -1)
              this.categories = [];

            var semester = {
              name: this.semesterName,
              status: this.status,

              semesterId: this.semesterId,
              catagories: this.categories
            }

            let loader = this.$loading.show({
              loader: 'spinner',
              color: '#0ACFE8'
            })
            this.$http.post("/api/semester/create", semester).then(response => {

              this.$toastr.s(response.status);

              this.config.data.push(response.data);

              this.semesters.push(response.data);

              console.log("submit response__", response);

            })
              .catch(error => {

                if (typeof error.response.data === "string") {

                  this.$toastr.e(error.response.data);

                } else {
                  this.$toastr.e(error.response.status);

                }
                // console.log("error__", error.response);

              })
              .then(function () {

                loader.hide();
              });

          }

        })
      },

      edit(row, root) {
        console.log("Row___________", row,this);
        //let loader = this.$loading.show({
        //  loader: 'spinner',
        //  color: '#0ACFE8'
        //});

        //setTimeout(function () {
        //  loader.hide();
        //}, 2000)

       // root.$toastr.s('success')

        this.style.boxCollapse = false;
        this.isEdit = true;
        this.semesterName = row.name;
        this.status = row.status;
        this.semesterId = row.semesterId;

        if (row.semesterId == -1) {

          this.categories = [];

          row.catagories.forEach((item) => {

            this.categories.push(item);
          })
        }

      },

      del(row) {

        var self = this;
        console.log("Row___________", row, this);

        this.style.confirm = true;

        this.confirmModal.callBack = function (root) {

          var index = self.config.data.findIndex((item) => {
            return row.id == item.id;
          })

          let loader = self.$loading.show({
            loader: 'spinner',
            color: '#0ACFE8'
          });

          self.$http.delete("/api/semester/delete/" + row.id)
            .then(function (response) {

            self.config.data.splice(index, 1);

            index = self.semesters.findIndex((item) => {
              return row.id == item.id;
            });

              self.semesters.splice(index, 1);

          })
            .catch(function () {

              root.$toastr.s('success')
            })
            .then(function () {
              loader.hide();
            });

          root.$emit('close');


        }

      }

    },

    computed: {

      newCategory() {
        return this.semesterId === -1;     
      }

    },

    components: {
      apptable: table,
      appconfirm: confirmModal
    }


  }

  function edit(row, root) {
    console.log("Row___________", row, this);
    let loader = root.$loading.show({
      loader: 'spinner',
      color: '#0ACFE8'
    });

    setTimeout(function () {
      loader.hide();
    }, 2000)
    root.$toastr.s('success')
  }

</script>

<style scoped>


</style>
