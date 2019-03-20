<template>

  <div>

    <div class="form-group">

      <label>{{ config.label }}</label>
      <select class="form-control" v-model="selected">
        <option v-for="option in config.data" :value="option.value">{{ option.text }}</option>
      </select>

    </div>

  </div>


</template>



<script>
  import { util } from '../mixins/util'
  export default {
    mixins: [util],
    props: {

      config: {

        type: Object,
        default: function () {

          return {

            data: [{ value: 1, text: 'Test' }],
            label: 'Test'
          }
        }
      },

      value: {

        type: Object,
        default: () => {

          return {
            value: 1,
            text: ''
          }
        }
      }
    },

    data() {
      return {

        selected: this.value.value

      }
    },

    watch: {

      value(newValue) {

        this.selected = newValue.value;
      },

      selected(newvalue) {

        var selectedObj = this.config.data.find(value => value.value == newvalue);

        if (!selectedObj) throw "Object not found";

        this.map(selectedObj, this.value);

        this.$emit('input', this.value);
        this.$emit('change')
      }

    }

  }


</script>


<style>

</style>
