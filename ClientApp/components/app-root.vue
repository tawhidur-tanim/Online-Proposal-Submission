<template>
  <div id="app" :class="wrraper">
    <!--<NavNew></NavNew>-->
    <router-view name="nav"></router-view>
    <div :class="contentWrapper">
      <appLoader v-if="isSpin"></appLoader>

      <router-view></router-view>

      <div class="content">
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
    </div>
    <div class="content-wrapper"></div>
  </div>
</template>

<script>

  import NavMenu from './nav-menu'
  import NavNew from './nav-new.vue'
  import spin from '../HelperComponents/spinner'
  import modal from '../HelperComponents/modal'
  import repo from '../Repositories/homeRepository'
  import bus from '../HelperComponents/Bus'

 // wrapper
    export default {
      components: {
        'nav-menu': NavMenu,
        NavNew: NavNew,
        appLoader: spin,
        modal
      },

      data () {
        return {
          passModal: false,

        }
    }, 

    created() {

      this.$store.dispatch('tryLogIn');
      bus.$on("passchange", this.passChange);
      bus.$on("error", (message) => {

        this.$toastr.e(message);
      });

    },
    mounted() {
    },

    computed: {

      wrraper() {

        return {

          wrapper: this.$route.name !== "login"
        }
      },
      contentWrapper() {

        return {
          'content-wrapper': this.$route.name !== "login"
        }
      },

      isSpin() {

        return this.$store.getters.isSpin;
      }
    },

    methods: {

      passChange() {

        this.passModal = true;

      },
      passEnter() {

        this.$validator.validateAll().then((result) => {

          if (!result) {
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
