<template>
  <div id="app" :class="wrraper">
    <!--<NavNew></NavNew>-->
    <router-view name="nav"></router-view>
    <div :class="contentWrapper">
      <appLoader v-if="isSpin"></appLoader>

      <router-view></router-view>
    </div>
    <div class="content-wrapper"></div>
  </div>
</template>

<script>
    import NavMenu from './nav-menu'
  import NavNew from './nav-new.vue'
  import spin from '../HelperComponents/spinner'
 // wrapper
    export default {
      components: {
        'nav-menu': NavMenu,
        NavNew: NavNew,
        appLoader: spin
      },

      data () {
        return {}
    }, 

    created() {

      this.$store.dispatch('tryLogIn');
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
    }
    }
</script>

<style>
</style>
