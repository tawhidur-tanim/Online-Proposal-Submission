<template>
  <div class="content">
    <div class="content-header">
      <h2>Proposals</h2>
    </div>

    <tab :tabs="tabNames">
      <div slot="Project">

      </div>
      <div slot="Thesis">
      </div>
      <div slot="Internship">
      </div>

    </tab>

  </div>

</template>

<script>
  import tab from '../HelperComponents/Tab'

  export default {

    components: {
      tab
    },

    data() {

      return {

        tabNames: ['Project', 'Thesis', 'Internship']

      }
    }
  }

</script>
