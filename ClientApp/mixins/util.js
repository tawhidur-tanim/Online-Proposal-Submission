export const  util = {


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
    }

  }

}
