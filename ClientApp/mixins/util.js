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
    },

    toTitleCase(str) {
      str = str.toLowerCase().split(' ');
      for (var i = 0; i < str.length; i++) {
        str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
      }
      return str.join(' ');
    },

    map(from, to) {

      if (!from || !to) {
        return;
      }

      Object.keys(from).forEach((key) => {

        if (to.hasOwnProperty(key)) {

          to[key] = from[key];
        }

      });
    },

    getDefaultValue(type) {

      switch (type) {
      case 'boolean': return false;
      case 'function': return function () { };
      case 'null': return null;
      case 'number': return 0;
      case 'object': return {};
      case 'string': return "";
      case 'symbol': return Symbol();
      case 'undefined': return void 0;
      }
    },

    emptyObject(obj) {

      if (!obj) return;

      Object.keys(obj).forEach((key) => {

        obj[key] = this.getDefaultValue(typeof obj[key]);

      });
    }

  }

}
