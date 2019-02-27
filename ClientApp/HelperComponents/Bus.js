import Vue from 'vue'

const  eventBus = new Vue({

  methods: {

    collapseBox() {

      this.$emit('collapseBox');
    }

  }

});


export default eventBus;
