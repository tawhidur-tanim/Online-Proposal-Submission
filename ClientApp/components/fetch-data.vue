<template>
  <div class="content">
    <div class="content-header">
      <h1>Weather forecast</h1>
    </div>

    <!--<appTable :tableConfig="config">
    </appTable>-->

    <!--<v-server-table url="/home/get" :columns="config.columns" :options="options" ></v-server-table>-->

    <appTab :tabs="['Project','Thesis','Internship']">
      <div slot="Project">1</div>
      <div slot="Thesis">2</div>
      <div slot="Internship">3</div>
    </appTab>
  </div>
</template>

<script>

  import table from '../HelperComponents/clientTable'
  import repo from '../Repositories/Proposals'
  import tab from '../HelperComponents/Tab'

  export default {
    computed: {
      totalPages: function () {
        return Math.ceil(this.total / this.pageSize)
      }
    },

    data() {
      return {
        forecasts: null,
        total: 0,
        pageSize: 5,
        currentPage: 1,

        config: {
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
          columns: ["name", "age"],
          actions: {
            Edit: {
              callBack: this.edit,
              cssClass: "btn-success"
            },
            Delete: {
              callBack: function (row) {
                console.log("Row___________", row, this);
              },
              cssClass: "btn-danger"
            }
          },

          templates: {

            age: function (row, h) {

              return h('button', { 'class': 'btn btn-warning btn-sm' }, row.age);

            }
          }
        },


        options: {}
      }
    },

    methods: {
      async loadPage(page) {
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

      showToast() {
        this.$toastr('success', 'i am a toastr success', 'hello')
      },
      edit(row, root) {
        console.log("Row___________", row);
        let loader = this.$loading.show({
          loader: 'spinner',
          color: '#0ACFE8'
        });

        setTimeout(function () {
          loader.hide();
        }, 2000)


        repo.getSemesters().then((data) => {


          console.log("from compo", data);
        })

        root.$toastr.s('success')
      }

    },

    async created() {
      this.loadPage(1)
    },
    beforeRouteEnter(to, from, next) {

      next(vm => {

        // console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token, to)

        if (vm.$store.getters.isAuthenticated) {

          return vm.$router.push({ name: to.name })
        }
        else {
          return vm.$router.push('/login')
        }

      })
    },
    components: {
      appTable: table,
      appTab: tab
    }

  }
</script>

<style>
</style>
