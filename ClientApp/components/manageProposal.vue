<template>

  <div>

    <div class="content">

      <div class="content-header">
        <h1>Manage proposals</h1>
      </div>

      <div class="row">
        <div class="col-md-2 my-1">
          <appSelect :config="select" v-model="selected" @change="changeSelect"></appSelect>
        </div>
        <div class="col-md-2 my-1">
          <appSelect :config="selectType" v-model="selectedType" @change="changeSelectType"></appSelect>
        </div>
      </div>



      <appTable :tableConfig="table"></appTable>

    </div>

  </div>
</template>

<script>
  import appTable from '../HelperComponents/clientTable'
  import repo from '../Repositories/manageProposal'
  import { util } from '../mixins/util'
  import appSelect from '../HelperComponents/select'

  export default {
    mixins: [util],
    created() {

      repo.getProposals().then(({ data }) => {

        this.table.data = this.mapper(data);
      })

      Object.keys(this.status).forEach((key) => {

        this.select.data.push({ value: key, text: this.status[key] })

      })

      Object.keys(this.types).forEach((key) => {

        this.selectType.data.push({ value: key, text: this.types[key] })

      })
      this.select.data.unshift({value: '', text: 'All'})
      this.selectType.data.unshift({value: '', text: 'All'})

    },

    components: {
      appTable,
      appSelect
    },

    data() {

      return {
        table: {
          data: [],
          filterable: ["studentName"],
          columns: ["studentName", "status", "type", "supervisorName", "reviewerName"],
          templates: {

            status:(row,h) => {

              return h('label', { 'class': 'label label-' + this.color[row.status] }, this.status[row.status])
            },

            type: (row, h) => {

              return h('label', { 'class': 'label label-success'}, this.types[row.type]);
            }

          },

          actions: {

            Details: {
              callBack: () => {}
            },

            Manage: {
              callBack: () => { },
              cssClass: 'btn btn-primary'
            }

          },

          customFilters: [{
            name: 'status',
            callback: function (row, query) {

              return row.status == query;
            }
          },
            {
              name: 'type',
              callback: function (row, query) {

                return row.type == query;
              }
            }]
        },

        select: {
          data: [],
          label: "Status"
        },

        selectType: {
          data: [],
          label: "Type"
        },

        selected: {value: '', text: ''},
        selectedType: {value: '', text: ''}
      }
    },

    methods: {

      mapper(data) {

        data.forEach((row) => {

          if (row.student && row.student.fullName) {
            row.studentName = row.student.fullName + "\n [" + row.student.userName + "]";
          } else {
            row.studentName = "No Name Found";
          }

          if (row.supervisor && row.supervisor.fullName) {
            row.supervisorName = row.supervisor.fullName + "\n [" + row.supervisor.userName + "]";

          } else {
            row.supervisorName = "No Name Found";
          }

          if (row.reviewer && row.reviewer.fullName) {
            row.reviewerName = row.reviewer.fullName + "\n [" + row.reviewer.userName + "]";

          } else {
            row.reviewerName = "No Name Found";
          }

        })


        return data;
      },

      changeSelect() {

        this.$tableEvent.$emit('vue-tables.filter::status', this.selected.value);
      },
      changeSelectType() {

        this.$tableEvent.$emit('vue-tables.filter::type', this.selectedType.value);

      }
    }
  }


</script>

<style>

</style>
