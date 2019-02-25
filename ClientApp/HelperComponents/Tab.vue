<template>
  <div>
    
    <div class="nav-tabs-custom">
      <ul class="nav nav-tabs">
        <li   v-for="name in tabs"  :class="{active: tab[name]}" @click="change(name)"><a >{{ name }}</a></li>    
      </ul>
      <div class="tab-content">
        <div class="tab-pane" v-for="name in tabs" :class="{active: tab[name]}">

          <slot :name="name">Demo</slot>
        </div>
      </div>
      <!-- /.tab-content -->
    </div>
  </div>
</template>

<script>
  export default {

    props: {
      tabs: {

        type: Array,
        default: ['Fisrt']
      }
    },
    mounted() {

      var len = this.tabs.length;
      for (var i = 0; i < len; i++) {

        this.$set(this.tab, this.tabs[i], false);
      }

      this.tab[this.tabs[0]] = true;

    },
    data() {

      return {

        tab: {
        }
      }
    },

    methods: {

      change(name) {

        var len = this.tabs.length;
        for (var i = 0; i < len; i++) {

          this.tab[this.tabs[i]] = false; 
        }

        this.tab[name] = true;

      }

    }

  }
</script>
<style scoped>
</style>
