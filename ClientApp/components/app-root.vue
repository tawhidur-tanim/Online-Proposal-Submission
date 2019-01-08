<template>
  <div id="app" :class="wrraper">
    <!--<NavNew></NavNew>-->
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

    mounted() {

      console.log(this.$route)
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
