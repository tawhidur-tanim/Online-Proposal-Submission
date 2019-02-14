export const util = {

  data() {

    return {

      loadConfig: {
        loader: 'spinner',
        color: '#0ACFE8'
      },

      loader: {}
    }
  },

  methods: {

    __copy(object) {

      if (!object) {

        return {};
      }

      let obj = {}
      Object.keys(object).forEach(function(key) {

        obj[key] = object[key];
      });

      return obj;
    },

    __arrayCopy(array) {

      if (!Array.isArray(array)) {

        return [];
      }

      var ara = [];

      // console.log("This of uti", array);
      array.forEach((item) => {

       // console.log(this.__copy(item));
        ara.push(this.__copy(item));
      });


      return ara;
    },

    loadShow() {

       this.loader = this.$loading.show({
        loader: 'spinner',
        color: '#0ACFE8'
      });

    },

    loadHide() {
      this.loader.hide();
    }

  }

}
