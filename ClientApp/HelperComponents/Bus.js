import Vue from 'vue'

const  eventBus = new Vue({

  methods: {

    collapseBox() {

      this.$emit('collapseBox');
    },

    openBox() {

      this.$emit('openBox');
    }

  }

});


export default eventBus;
