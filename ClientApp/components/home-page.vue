<template>
    <div class="content">

      <box :icon="false">
        <template slot="body">
          <div class="row">
            <div class="col-md-6">
              <table class="table table-hover table-bordered">
                <tbody>
                  <tr>
                    <td>Supervisor: </td>
                    <td>XXX</td>
                  </tr>
                  <tr>
                    <td>Reviewer: </td>
                    <td>XXX</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </template>
      </box>

    </div>
</template>

<script>

  import box from '../HelperComponents/box'

export default {
  data () {
    return {}
    },

    beforeRouteEnter(to, from, next) {

      next(vm => {

        console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token,to)

        if (vm.$store.getters.isAuthenticated) {

          return vm.$router.push({name: to.name})
        }
        else {
          return vm.$router.push('/login')
        }

      })
    },

    components: {
      box
    }
}
</script>

<style>
</style>
