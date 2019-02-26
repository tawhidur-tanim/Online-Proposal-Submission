<template>
  <div>

    <div class="row">

      <div :class="'col-md-'+ (form.template[key].colSpan ? form.template[key].colSpan : form.col)" v-for="(value, key) in form.form">
        <div></div>
      </div>

    </div>

  </div>

</template>

<script>

  export default {

    props: {

      config: {

        type: Object,

        default: function () {
          return {

            form: {
              x: 'x'
            },

            template: {

              x: {
                type: 'textarea',
                row: 2,
                col: 2,
                //colSpan: 3,

                validation: 'required'
              }
            },

            col: 2

          }
        }
      }
    },

    computed: {

      form() {

        this.config.col = (Math.round(12 / (this.config.col % 12))) % 12

        Object.keys(this.config.form).forEach((item) => {

          if (this.config.template[item] && !this.config.template[item].type) {

            this.config.template[item].type = 'text'
          }

          if (this.config.template[item] && this.config.template[item].colSpan) {

            this.config.template[item].colSpan = this.config.template[item].colSpan % 13;
          }

          return this.config;

        })

      }
    }

  }


</script>
