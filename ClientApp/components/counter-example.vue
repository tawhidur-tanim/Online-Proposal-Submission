<template>
    <div class="content">
        <h1 class="content-header">Counter</h1>

        <p>This is a simple example of a Vue.js component & Vuex</p>

        <p>
            Current count (Vuex): <strong>{{ currentCount }}</strong>
        </p>
        <p>
            Auto count: <strong>{{ autoCount }}</strong>
        </p>

      <p>
            Auto count: <strong>{{ date }}</strong>
        </p>

        <button type="button" class="btn btn-primary" @click="incrementCounter()">Increment</button>
        <button type="button" class="btn btn-secondary" @click="resetCounter()">Reset</button>
    </div>
</template>

<script>
    import { mapActions, mapState } from 'vuex'

    export default {
      data () {
        return {
          autoCount: 0
        }
      },

      computed: {
        ...mapState({
          currentCount: state => state.counter
        }),

        date() {

          var expireDate = new Date(localStorage.getItem('expireTime'));

          console.log(expireDate, new Date(), localStorage.getItem('expireTime'))

          return this.$moment(expireDate).diff(new Date(), 'seconds')
        }
      },

      methods: {
        ...mapActions(['setCounter']),

        incrementCounter: function () {
          var counter = this.currentCount + 1
          this.setCounter({counter: counter})
        },

        resetCounter: function () {
          this.setCounter({counter: 0})
          this.autoCount = 0
        }
      },

      created () {
        setInterval(() => {
          this.autoCount += 1
        }, 1000)
    },
    beforeRouteEnter(to, from, next) {

      next(vm => {

        console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token, to)
        if (vm.$store.getters.isAuthenticated) {

          return vm.$router.push({ name: to.name })
        }
        else {
          return vm.$router.push('/login')
        }

      })
    }
    }
</script>

<style>
</style>
