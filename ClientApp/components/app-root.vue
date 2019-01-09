<template>
  <div id="app" :class="wrraper">
    <!--<NavNew></NavNew>-->
    <router-view name="nav"></router-view>
    <div :class="contentWrapper">
      <router-view></router-view>
    </div>
    <div class="content-wrapper"></div>
  </div>
</template>

<script>
    import NavMenu from './nav-menu'
    import NavNew from './nav-new.vue'
 // wrapper
    export default {
      components: {
        'nav-menu': NavMenu,
        NavNew : NavNew 
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
      }
    }
    }
</script>

<style>
</style>
