<template>

  <div class="content">

    <box :icon="false">


      <template slot="body">
        <div class="form-group">
          <div class="row">

            <div class="col-md-2">
              <label>Teachers List: </label>
            </div>
            <div class="col-md-3">
              <input type="file" class="form-control" ref="teachers" v-validate="'ext:xlsx|required'" accept=".xlsx" name="teachers"/>
            </div>
            <div class="col-md-3">
              <button class="btn btn-success" @click="SaveTeachers">Save</button>
            </div>

          </div>
        </div>


        <div class="form-group">
          <div class="row">

            <div class="col-md-2">
              <label>Course List: </label>
            </div>
            <div class="col-md-3">
              <input type="file" class="form-control" ref="courses" name="courses" v-validate="'ext:xlsx|required'" accept=".xlsx" />
            </div>
            <div class="col-md-3">
              <button class="btn btn-success" @click="SaveCourses">Save</button>
            </div>

          </div>
        </div>
      </template>

    </box>
    <div class="row">

      <div class="col-md-2 mb25">
        <button class="btn btn-info" @click="createModal = true">Add New Course</button>
      </div>
    </div>
    <appTable :tableConfig="config"></appTable>

    <confirmModal :config="confirmModal" v-if="confirmShow" @close="confirmShow = false"></confirmModal>

    <modal  v-if="createModal" @close="createModal = false" >

      <template slot="header">
        <h3>New Course</h3>
      </template>

      <template slot="body">
        <div class="form-group">
          <label>Course Title</label>
          <input class="form-control" name="title" v-validate="'required'" :class="{'error': errors.has('create.title')}"
                 v-model="course.title" data-vv-scope="create" />
        </div>

        <div class="form-group">
          <label>Course Code</label>
          <input class="form-control" name="code" v-validate="'required'" :class="{'error': errors.has('create.code')}"
                 v-model="course.courseCode" data-vv-scope="create" />
        </div>

        <div class="form-group">
          <label>Course Credit</label>
          <input class="form-control" name="credit" v-validate="'required|numeric'" :class="{'error': errors.has('create.credit')}"
                 v-model="course.credit" data-vv-scope="create" />
        </div>
      </template>

      <template slot="footer">
        <button class="btn btn-success" @click="addCourse">Add</button>
      </template>
    </modal>

  </div>

</template>


<script>

  import box from '../HelperComponents/box'
  import appTable from '../HelperComponents/clientTable'
  import confirmModal from '../HelperComponents/confirmModal'
  import modal from '../HelperComponents/modal'
  import repo from '../Repositories/settingsRepository'

  export default {

    components: {
      box,
      appTable,
      confirmModal,
      modal
    },

    created() {

      repo.getAllCourses().then(({ data}) => {

        this.config.data = data;

      })

    },
    data() {

      return {

        file: null,

        config: {
          data: [],
          columns: ["title", "courseCode","credit"],
          actions: {
            //Edit: {
            //  callBack: this.edit,
            //  cssClass: "btn-success btn-sm"
            //},
            Delete: {
              callBack: this.del,
              cssClass: "btn-danger btn-sm"
            }
          },

          templates: {}
        },
        confirmModal: {
          callBack: (root) => {

            setTimeout(() => {

              root.$emit('close')

            }, 2000)

          }
        },

        course: {

          title: '',

          courseCode: '',

          credit: ''
        },
        confirmShow: false,
        createModal: false
      }
    },
    methods: {

      SaveTeachers() {

        this.file = this.$refs.teachers.files[0];

        repo.saveTeachers(this.file).then(() => {

          this.$toastr.s("Success");

        })

      },

      SaveCourses() {

        this.file = this.$refs.courses.files[0];

        repo.saveCourses(this.file).then(() => {

          this.$toastr.s("Success");
          repo.getAllCourses().then(({ data }) => {

            this.config.data = data;

          })


        })

      },

      edit() {
      },
      del(row) {

        var self = this;

        this.confirmShow = true;

        this.confirmModal.callBack = function (root) {

          var index = self.config.data.findIndex((item) => {
            return row.id == item.id;
          })

          repo.deleteCourse(row.id).then(() => {

            self.$toastr.s("Course Deleted");

            self.config.data.splice(index, 1);

            root.$emit('close');

          })

        }

      },

      addCourse() {

        this.$validator.validateAll('create').then((result) => {

          if (!result) return;


          repo.addCourse(this.course).then(({ data }) => {

            this.$toastr.s("Success");

            this.config.data.push(data);

            this.createModal = false;

          })

        })

      }
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
    }

  }


</script>
