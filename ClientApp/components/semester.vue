<template>

  <div class="content">
    <div class="content-header">
      <h2>Semester Manage</h2>
    </div>

    <div class="box box-default">
      <div class="box-header with-border">
        <h3 class="box-title">Default Box Example</h3>
        <div class="box-tools pull-right">
          <!-- Buttons, labels, and many other things can be placed here! -->
          <!-- Here is a label for example -->
          <span class="label label-primary">Label</span>
        </div>
        <!-- /.box-tools -->
      </div>
      <!-- /.box-header -->
      <div class="box-body">
        The body of the box


      </div>
      <!-- /.box-body -->
      <div class="box-footer">
        The footer of the box
      </div>
    </div>
  </div>

</template>

<script>
  export default {

    created() {

      this.$http.get("/api/semester/getall").then(response => {

        console.log(response);

      })
    }

  }
</script>

<style>


</style>
