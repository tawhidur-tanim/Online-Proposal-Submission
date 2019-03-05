export const util = {

  data() {

    return {

      loadConfig: {
        loader: 'spinner',
        color: '#0ACFE8'
      },

      loader: null
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

    toggleLoader() {

      this.$store.commit('toggleLoader');

    },

    error(error) {
      if (typeof error.response.data === "string") {

        this.$toastr.e(error.response.data);

      } else {
        this.$toastr.e(error.response.status);

      }
    }

  }

}
