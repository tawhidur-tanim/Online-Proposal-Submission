<template>
  <div class="content">
    <div class="content-header">
      <h1>Weather forecast</h1>
    </div>

    <appTable :tableConfig="config">
    </appTable>

  </div>
</template>

<script>

import table from './clientTable.vue'

export default {
  computed: {
    totalPages: function () {
      return Math.ceil(this.total / this.pageSize)
    }
  },

  data () {
    return {
      forecasts: null,
      total: 0,
      pageSize: 5,
      currentPage: 1,

      config:{
        data: [
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        {
          name: 'Tanim',
          age: 23
        },
        ],
        columns: ["name","age"],
        actions:{
          Edit: {
            callBack: function(row,root){
                console.log("Row___________",row);
            root.$toastr.s('success')
            },
            cssClass: "btn-success"
          },
          Delete: {
            callBack: function(row){
                console.log("Row___________",row,);
            },
            cssClass: "btn-danger"
          }
        },

        templates:{

          age: function(row,h){

            return h('button', { 'class': 'btn btn-warning btn-sm' }, row.age);

          }
        }
      },


     
    }
  },

  methods: {
    async loadPage (page) {
      // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
      // TypeScript can also transpile async/await down to ES5
      this.currentPage = page

      try {
        var from = (page - 1) * (this.pageSize)
        var to = from + this.pageSize
        let response = await this.$http.get(`/api/weather/forecasts?from=${from}&to=${to}`)
        console.log(response.data.forecasts)
        this.forecasts = response.data.forecasts
        this.total = response.data.total
      } catch (err) {
        window.alert(err)
        console.log(err)
      }
      // Old promise-based approach
      // this.$http
      //    .get('/api/SampleData/WeatherForecasts')
      //    .then(response => {
      //        console.log(response.data)
      //        this.forecasts = response.data
      //    })
      //    .catch((error) => console.log(error))*/
    },

    showToast(){
      this.$toastr('success', 'i am a toastr success', 'hello')
    }
  },

  async created () {
    this.loadPage(1)
  },

  components:{
    appTable: table
  }
}
</script>

<style>
</style>
