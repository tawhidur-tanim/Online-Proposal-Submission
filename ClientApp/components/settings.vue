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
              <input type="file" class="form-control" ref="teachers"/>
            </div>

            <div class="col-md-3">
              <button class="btn btn-success" @click="SaveTeachers" >Save</button>
            </div>
            
          </div>

        </div>

      </template>

    </box>


  </div>

</template>


<script>

  import box from '../HelperComponents/box'
  import repo from '../Repositories/settingsRepository'

  export default {

    components: {
      box
    },
    data() {

      return {

        file: null
      }
    },
    methods: {

      SaveTeachers() {

        this.file = this.$refs.teachers.files[0];

        repo.saveTeachers(this.file).then(() => {

          this.$toastr.s("Success");

        })

      }
    }

  }


</script>
