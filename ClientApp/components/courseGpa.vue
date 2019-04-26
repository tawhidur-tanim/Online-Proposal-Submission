<template>

  <div class="content">

    <box :icon="false">
      <template slot="title">
        <button class="btn btn-success" @click="saveGpa">Save</button>
      </template>
      <template slot="body">
        <table class="table table-bordered">
          <thead>
            <tr>
              <th>Course Name</th>
              <th>Course Code</th>
              <th>Credit</th>
              <th>GPA</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="course in courseList">
              <td>{{ course.title }}</td>
              <td>{{ course.courseCode }}</td>
              <td>{{ course.credit }}</td>
              <td>

                <template v-if="course.isReadOnly">
                  <input type="text"
                         :name="course.courseCode+'-'+course.id"
                         v-validate="{required: true, regex: /^[-+]?\d+(\.\d+)?$/, min_value:1, max_value:4}"
                         class="form-control" style="width: 25%"
                         :class="{'error': errors.has(course.courseCode+'-'+course.id) }" v-model="course.gpa"
                         readonly />
                </template>

                <template v-else>
                  <input type="text"
                         :name="course.courseCode+'-'+course.id"
                         v-validate="{required: true, regex: /^[-+]?\d+(\.\d+)?$/, min_value:1, max_value:4}"
                         class="form-control" style="width: 25%"
                         :class="{'error': errors.has(course.courseCode+'-'+course.id) }" v-model="course.gpa"
                          />
                </template>
        
              </td>
            </tr>
          </tbody>

        </table>
      </template>

    </box>

    

  </div>

</template>


<script>

  import repo from '../Repositories/gpaRepository'
  import box from '../HelperComponents/box'

  export default {

    components: {
      box
    },
    created() {

      this.reloadTable();
    },
    data() {

      return {

        courseList: []
      }
    },

    methods: {

      saveGpa() {

        this.$validator.validateAll().then((result) => {

          if (!result) return;

          repo.saveGpa(this.courseList).then(() => {

            this.$toastr.s("Success");

            this.reloadTable();

          });

        });


      },

      reloadTable() {
        repo.getCourses().then(({ data }) => {

          this.courseList = data;

        })
      }

    }

  }

</script>
