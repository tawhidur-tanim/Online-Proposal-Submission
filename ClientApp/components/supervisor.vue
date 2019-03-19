<template>
  <div class="content">
    <appTable :tableConfig="table"></appTable>
  </div>
</template>

<script>

  import appTable from '../HelperComponents/clientTable'
  import roles from '../rolesConstant'
  import repo from '../Repositories/supervisorRepository'

  export default {

    beforeRouteEnter(to, from, next) {

      next(vm => {

        // console.log(vm.$store.getters.isAuthenticated, vm.$store.getters.state, vm.$store.getters.token, vm.$store.state.Auth.token, to)

        if (vm.$store.getters.isAuthenticated && (vm.$store.getters.role == roles.teacher || vm.$store.getters.role == roles.admin)) {

          return vm.$router.push({ name: to.name })
        }
        else {
          return vm.$router.push('/login')
        }

      })
    },

    created() {

      var userId = this.$store.getters.getUserId;

      repo.getStudentsBySupervisor(userId).then(({ data}) => {
        this.table.data = data;

      })

    },

    components: {
      appTable
    },

    data() {

      return {

        table: {
          data: [],
          filterable: ["fullName"],
          columns: ["fullName", "userName"],
          templates: {},
          actions: {},
        }

      }
    }

  }

</script>
<style>

</style>
