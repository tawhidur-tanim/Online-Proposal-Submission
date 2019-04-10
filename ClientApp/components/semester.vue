<template>
  <div class="content">
    <div class="content-header">
      <h2>Semester Manage</h2>
    </div>

    <box>
      <template slot="title">
        {{  isEdit ? "Semester Edit" : "Semester Create"   }}
      </template>

      <template slot="body">
        <form class="form-horizontal">
          <div class="form-group">
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
              <select class="form-control w25" id="category" v-model="semesterId">
                <option v-for="semester in semesters" :value="semester.id">{{semester.name}}</option>
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
              <div class="col-md-2 col-sm-offset-2">
                <label>Category Name</label>
                <input type="text" class="form-control" v-model="cat.name" :name="'name'+cat.id" v-validate="'required'" :class="{'error': errors.has('name'+cat.id)}" />
              </div>
              <div class="col-md-2">
                <label>Marks</label>
                <input type="text" class="form-control" v-model="cat.mark" :name="'mark'+cat.id" v-validate="'required|max_value:100|min_value:1'" :class="{'error': errors.has('mark'+cat.id)}" />
              </div>
              <div class="col-md-2">
                <label>Mark Type: </label>
                <select class="form-control" v-model="cat.markType" :name="'markType'+cat.id" v-validate="'required'" :class="{'error': errors.has('markType'+cat.id)}">
                  <option value="1">Supervisor</option>
                  <option value="2">Reviewer</option>
                </select>
              </div>
              <div class="col-md-2">
                <button class="btn btn-danger  mt25 ml25i" v-on:click.prevent="deleteCat(cat.id)"><i class="fa fa-times"></i></button>
              </div>

            </div>
          </div>

          <div class="form-group" v-if="!isEdit">
            <label for="studnets" class="col-sm-2 control-label">Student List</label>
            <div class="col-sm-10">
              <input type="file" class="form-control w25" id="studnets" ref="file" v-validate="'ext:xlsx|required'" name="studentList"
                     data-vv-as="Student List">
              <span style="color:red">{{ errors.first('studentList') }}</span>
            </div>
          </div>

          <div class="form-group">
            <div class="col-sm-offset-2 col-sm-10">
              <button type="submit" class="btn btn-success" v-on:click.prevent="submit(createUrl,addRow)" v-if="!isEdit">Create</button>

              <template v-if="isEdit">
                <button type="submit" class="btn btn-primary" v-on:click.prevent="submit(editUrl,updateRow)">Update</button>
                <button type="submit" class="btn btn-default" v-on:click.prevent="reset">Reset</button>
              </template>

            </div>
          </div>


        </form>
      </template>
    </box>

    <appconfirm v-if="style.confirm" @close="style.confirm = false" :config="confirmModal"></appconfirm>

    <apptable :tableConfig="config">
    </apptable>

  </div>
</template>
<script>

  import table from '../HelperComponents/clientTable'
  import confirmModal from '../HelperComponents/confirmModal'
  import { util } from '../mixins/util'
  import roles from '../rolesConstant'
  import box from '../HelperComponents/box'
  import Bus from '../HelperComponents/Bus'

  export default {

    mixins: [util],
    created() {

      this.toggleLoader();

      this.$http.get("/api/semester/semesters").then(response => {
        this.semesters = this.__arrayCopy(response.data)
        this.config.data = response.data;

        this.semesters.unshift({ id: -1, name: "New" });

      })
        .catch(error => { })
        .then(response => {
          this.toggleLoader();
        });
    },
    data() {

      return {
        style: {
          boxCollapse: true,
          confirm: false
        },

        id: 0,
        isEdit: false,
        semesterId: -1,
        semesterName: '',
        status: 1,
        semesters: [],
        categories: [
          { name: '', mark: 0, id: 1, markType: 1 }
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
        },

        createUrl: "/api/semester/create",
        editUrl: "/api/semester/update"
      }
    },

    methods: {
      deleteCat(id) {
        var index = this.categories.findIndex(x => x.id === id);
        this.categories.splice(index, 1);
      },

      addCat() {

        if (this.categories.length === 0) {
          this.categories.push({ name: '', mark: 0, id: 1, markType: 1 });
          return;
        }

        var id = this.categories[this.categories.length - 1].id;

        this.categories.push({
          name: '',
          mark: 0,
          id: ++id,
          markType: 1
        });
      },

      reset() {

        this.isEdit = false;

        this.categories = [
          { name: '', mark: 0, id: 1, markType: 1}
        ];
        this.semesterId = -1;
        this.status = 1;
        this.semesterName = '';
      },


      submit(url, action) {

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
              id: this.id,
              name: this.semesterName,
              status: this.status,

              semesterId: this.semesterId,
              catagories: this.categories
            }

            this.toggleLoader();

            this.$http.post(url, semester).then(response => {

              this.$toastr.s(response.status);

              action(response);

            })
              .catch(error => {

                if (typeof error.response.data === "string") {

                  this.$toastr.e(error.response.data);

                } else {
                  this.$toastr.e(error.response.status);

                }

              })
              .then(() => {
               // console.log("Entered in then");
                this.toggleLoader()

              });

          }

        })
      },

      edit(row, root) {

        Bus.openBox();

        this.isEdit = true;
        this.semesterName = row.name;
        this.status = row.status;
        this.semesterId = row.semesterId;
        this.id = row.id;

        if (row.semesterId == -1) {
          this.categories = [];
          row.catagories.forEach((item) => {

            this.categories.push(item);
          })
        }
        // document.body.scrollTop = document.documentElement.scrollTop = 0;
      },

      del(row) {

        var self = this;

        this.style.confirm = true;

        this.confirmModal.callBack = function (root) {

          var index = self.config.data.findIndex((item) => {
            return row.id == item.id;
          })

          root.toggleLoader();

          self.$http.delete("/api/semester/delete/" + row.id)
            .then(function (response) {

              self.config.data.splice(index, 1);

              index = self.semesters.findIndex((item) => {
                return row.id == item.id;
              });

              self.semesters.splice(index, 1);
            })
            .catch(function () {

              root.$toastr.e('error')
            })
            .then(function () {
              root.toggleLoader();

            });

          root.$emit('close');

        }

      },

      updateRow({ data }) {

        var semester = this.semesters.find(el => el.id == data.id);

        if (semester) {
          semester.name = data.name;
          semester.status = data.status;
          semester.semesterId = data.semesterId;
          semester.catagories = data.catagories;
        }

        semester = this.config.data.find(el => el.id == data.id)

        if (semester) {
          semester.name = data.name;
          semester.status = data.status;
          semester.semesterId = data.semesterId;
          semester.catagories = data.catagories;
        }
      },

      addRow(response) {
        this.config.data.push(response.data);
        this.semesters.push(response.data);

        this.toggleLoader();

        var form = new FormData();

        form.append("file", this.$refs.file.files[0])
        form.append("semesterId", response.data.id);

        this.$http.post("/api/semester/students", form,
          {
            headers: {
              'Content-Type': 'multipart/form-data'
            }
          })
          .then(() => {
          })
          .catch((error) => {
            console.log("File Error", error);

            this.$toastr.e('Could not upload file');
          })
          .then(() => {
            console.log("Entered in then 2");
            this.toggleLoader();

          });

      }
    },

    computed: {

      newCategory() {
        return this.semesterId === -1;
      }

    },

    components: {
      apptable: table,
      appconfirm: confirmModal,
      box
    },

    beforeRouteEnter(to, from, next) {

      next(vm => {

        // console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token, to)

        if (vm.$store.getters.isAuthenticated && vm.$store.getters.role == roles.admin) {

          return vm.$router.push({ name: to.name })
        }
        else {
          return vm.$router.push('/login')
        }

      })
    },
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
