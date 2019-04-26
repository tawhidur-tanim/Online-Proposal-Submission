<template>
    <div class="content">

      <template v-if="role == roles.admin">
        <div class="row">

          <div class="col-md-4">

            <div class="info-box">

              <span class="info-box-icon bg-green"><i class="fa fa-graduation-cap"></i></span>
              <div class="info-box-content">
                <span class="info-box-text">Students</span>
                <span class="info-box-number">{{ stats.currentStudents }}</span>
              </div>
            </div>
          </div>

          <div class="col-md-4">

            <div class="info-box">

              <span class="info-box-icon bg-blue"><i class="fa fa-file-text-o"></i></span>
              <div class="info-box-content">
                <span class="info-box-text">Proposals</span>
                <span class="info-box-number">{{ stats.totalProposal}} </span>
              </div>
            </div>
          </div>

          <div class="col-md-4">

            <div class="info-box">

              <span class="info-box-icon bg-purple"><i class="fa fa-check"></i></span>
              <div class="info-box-content">
                <span class="info-box-text">Accepted Proposals</span>
                <span class="info-box-number">{{ stats.acceptedProposal}} </span>
              </div>
            </div>
          </div>
        </div>

      </template>

      <box :icon="false">

        <template slot="title">
          Current Semester: {{ dash.currentSemesterName}}
        </template>
        
        <template slot="body">
          <div class="row">
            <div class="col-md-6">
              <table class="table table-hover table-bordered">
                <tbody>

                  <template v-if="roles.student === role">
                    <tr>
                      <td>Supervisor: </td>
                      <td>{{dash.supervisor}}</td>
                    </tr>
                    <tr>
                      <td>Reviewer: </td>
                      <td>{{dash.reviewer}}</td>
                    </tr>
                  </template>

                  <template v-else>
                    <tr>
                      <td>No. of student assigned: </td>
                      <td>{{dash.numberOfStudents}}</td>
                    </tr>
                  </template>

                </tbody>
              </table>
            </div>
          </div>
        </template>
      </box>

      <modal v-if="passModal" @close="passModal = false">

        <template slot="header">
          <h3>Password Change</h3>
        </template>

        <template slot="body">

          <div class="form-group">
            <label>Old Password</label>
            <input class="form-control" type="password"
                   v-model="oldPass" v-validate="'required'" name="password1"
                   :class="{'error': errors.has('password1')}" data-vv-as="Password" />
            <span class="error-font">{{ errors.first('password1') }}</span>
          </div>

          <div class="form-group">
            <label>New Password</label>
            <input class="form-control" type="password"
                   v-model="newPass" v-validate="'required|min:6'" ref="pass" name="password"
                   :class="{'error': errors.has('password')}" data-vv-as="New Password" />
            <span class="error-font">{{ errors.first('password') }}</span>
          </div>

          <div class="form-group">
            <label>Confirm Password</label>
            <input class="form-control" type="password" v-model="confirmPass"
                   v-validate="'required|confirmed:pass'" name="confirmPassword" data-vv-as="Confirm Password"
                   :class="{'error': errors.has('confirmPassword')}" />
            <span class="error-font">{{ errors.first('confirmPassword') }}</span>
          </div>

        </template>

        <template slot="footer">
          <button class="btn btn-success" @click="passEnter">Change</button>
        </template>

      </modal>




    </div>
</template>

<script>

  import box from '../HelperComponents/box'
  import repo from '../Repositories/homeRepository'
  import roles from '../rolesConstant'
  import modal from '../HelperComponents/modal'
  import { util } from '../mixins/util'

  export default {
    mixins: [util],
    data() {
      return {

        dash: {},
        roles: roles,
        passModal: false,
        oldPass: '',
        newPass: '',
        confirmPass: '',

        stats: {

          acceptedProposal: 0,
          totalProposal: 0,
          currentStudents: 0
        }
      }
    },

    computed: {

      role() {

        return this.$store.getters.role;
      }

    },

    beforeRouteEnter(to, from, next) {

      next(vm => {

        console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token,to)

        if (vm.$store.getters.isAuthenticated) {

          return vm.$router.push({name: to.name})
        }
        else {
          return vm.$router.push('/login')
        }

      })
    },

    components: {
      box,
      modal
    },

    created() {

      if (this.$store.getters.isAuthenticated) {
        repo.getDash().then(({ data }) => {

          this.dash = data;

        })

      //  bus.$on("passchange", this.passChange);

        repo.getStats().then(({ data }) => {

          this.map(data, this.stats);

        }) 
      }


    },

    methods: {

      passChange() {

        this.passModal = true;

      },
      passEnter() {

        this.$validator.validateAll().then((result) => {

          if(!result) {
            return;
          }

          repo.passChange({ password: this.oldPass, newPassword: this.newPass, confirmPassword: this.confirmPass })
            .then(() => {

              this.$toastr.s("Password Changed");

            })

        })


      }
    }

}
</script>

<style>
</style>
