<template>
  <div class="">

<div class="box box-default">
  <div class="box-body pad table-responsive">
<v-client-table :data="tableConfig.data" :options="options" :columns="columns">
      <div         slot="actions"
                slot-scope="{row}"
        >
        <button
        v-for="(data, key) in tableConfig.actions"
        :key="key"
        class="btn"
        :class="data.cssClass"
        @click="callBack(data.callBack,row)" style="margin-right: 3px;"
      >{{key}}</button>

      </div>
      
    </v-client-table>
  </div>
</div>

    
  </div>
</template>

<script>
export default {
  props: {
    tableConfig: {
      type: Object,
      default: () => ({
        data: [],
        actions: {},
        templates: {},
        columns: []
      })
    }
  },
  data: () => ({}),
  computed: {
    columns() {
      if (this.tableConfig.columns instanceof Array) {
        return this.tableConfig.columns;
      }

      if (typeof this.tableConfig.columns === "object") {
        var columns = [];

        Object.keys(this.tableConfig.columns).forEach(function(key) {
          columns.push(key);
        });

        return columns;
      }

      return [];
    },

    options() {
      var options = {
        headings: {},
        sortable: [],
        templates: {}
      };
      var self = this;
      var actionsFlag = false;

      if (Object.keys(self.tableConfig.actions).length) {
        actionsFlag = true;
      }

      if (
        typeof self.tableConfig.columns === "object" &&
        !(self.tableConfig.columns instanceof Array)
      ) {
        if (actionsFlag) {
          self.tableConfig.columns["actions"] = "Actions";
        }
        Object.keys(this.tableConfig.columns).forEach(function(key) {
          options.headings[key] = self.tableConfig.columns[key];

          options.sortable.push(key);
          options.filterable.push(key);
        });
      } else if (self.tableConfig.columns instanceof Array) {
           if (actionsFlag) {
          self.tableConfig.columns.push("actions")
        }
        options.sortable = self.tableConfig.columns;
        options.filterable = self.tableConfig.columns;
      }

      if (self.tableConfig.sortable) {
        options.sortable = self.tableConfig.sortable;
      }

      if (self.tableConfig.filterable) {
        options.filterable = self.tableConfig.filterable;
      }

      if (typeof self.tableConfig.templates === "object") {
        Object.keys(self.tableConfig.templates).forEach(function(key) {

          options.templates[key] = function (h, row, index) {
            // return h('button', { 'class': 'btn btn-warning btn-sm' }, row.age);

            return self.tableConfig.templates[key](row, h, index);
          };

          console.log(self.tableConfig.templates[key]);
        });


        // options.templates["age"] = function(h, row, index) {
        //     return h('button', { 'class': 'btn btn-warning btn-sm' }, row.age);

        //     //return self.tableConfig.templates[key](row, h, index);
        //   };
        console.log(options);
      }

      return options;
    }
  },

  methods:{

    callBack(func,row){

      func(row,this);
    }
  }
};
</script>

<style scoped>
</style>
