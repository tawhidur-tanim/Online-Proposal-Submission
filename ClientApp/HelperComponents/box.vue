<template>

  <div>
    <div class="box box-default" :class="{'collapsed-box': boxCollapse}">
      <div class="box-header with-border">
        <h3 class="box-title">
          <slot name="title"></slot>
        </h3>
        <div class="box-tools pull-right" v-if="icon">
          <!-- Buttons, labels, and many other things can be placed here! -->
          <!-- Here is a label for example -->
          <button class="btn btn-box-tool" @click="boxCollapse = !boxCollapse">
            <i :class="{'fa-plus': boxCollapse, 'fa-minus': !boxCollapse}" class="fa"></i>
          </button>
        </div>
        <!-- /.box-tools -->
      </div>
      <!-- /.box-header -->
      <div class="box-body">
        <slot name="body"></slot>
      </div>
      <!-- /.box-body -->
      <div class="box-footer">
       <slot name="footer"></slot>
      </div>
    </div>
  </div>


</template>




<script>

  import Bus from './Bus'
  export default {

    props: {

      icon: {

        type: Boolean,
        default: true
      }

    },

    created() {

      Bus.$on('collapseBox', () => {

        this.boxCollapse = false;

      });

    },

    data() {

      return {
        boxCollapse: true

      }
    }

  }

</script>
