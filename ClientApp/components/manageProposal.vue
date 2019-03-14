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

      <modal v-if="detailsModal" @close="detailsModal = false" cls="md">

        <template slot="header">
          <h3>Proposal Details</h3>   
        </template>

        <template slot="body">
          <div class="row">
            <div class="col-md-2">
              <label>Student Name</label>
            </div>
            <div class="col-md-3">
              <span v-if="proposal.student">{{ proposal.student.fullName }}</span>
              <span v-else>{{ intern.student.fullName }}</span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-2">
              <label>Student Id</label>
            </div>
            <div class="col-md-3">
              <span v-if="proposal.student">{{ proposal.student.userName }}</span>
              <span v-else>{{ intern.student.userName }}</span>
            </div>
          </div>
          <hr />
          <div class="row">
            <div v-if="modalData === 1 || modalData === 3">
              <div class="col-md-6">
                <label>Title</label>
                <span>{{ proposal.title }}</span>
              </div>

              <div class="col-md-6">
                <label>Area Of Study</label>
                <span>{{ proposal.areaOfStudy }}</span>
              </div>

              <div class="col-md-6">
                <label>Description</label>
                <span>{{ proposal.description }}</span>
              </div>

              <div class="col-md-6">
                <label>Type</label>
                <span>{{  types[proposal.type] }}</span>
              </div>
            </div>
            <div v-if="modalData === 2">
              <div class="col-md-6">
                <label>Language Skill</label>
                <span>{{ intern.language }}</span>
              </div>

              <div class="col-md-6">
                <label>Known Framework</label>
                <span>{{ intern.frameWorkDescription }}</span>
              </div>

              <div class="col-md-6">
                <label>Reason of interest</label>
                <span>{{ intern.internshipReason }}</span>
              </div>

              <div class="col-md-6">
                <label>Already have internship</label>
                <span>{{  intern.isHaveInternship ? "Yes" : "No" }}</span>
              </div>
              <template v-if=" intern.isHaveInternship">
                <div class="col-md-6">
                  <label>Company Name</label>
                  <span>{{  intern.companyName }}</span>
                </div>
                <div class="col-md-6">
                  <label>Company address</label>
                  <span>{{  intern.companyAddress }}</span>
                </div>
                <div class="col-md-6">
                  <label>Job Description</label>
                  <span>{{  intern.jobDescriotion }}</span>
                </div>
                <div class="col-md-6">
                  <label>Internship Rreference</label>
                  <span>{{  intern.internshipRefernce }}</span>
                </div>
                <div class="col-md-6">
                  <label>Contact of supervisor</label>
                  <span>{{  intern.contactForSupervisor }}</span>
                </div>
              </template>
            </div>
          </div>
        </template>

      </modal>

      <modal v-if="manageModal" @close="manageModal = false">
        <template slot="header">
          <h3>Manage</h3>
        </template>

        <template slot="body">
          <appSelect :config="statusSelect"></appSelect>
          <button @click="confirmStatus">Confirm</button>
        </template>
      </modal>

    </div>

  </div>
</template>

<script>
  import appTable from '../HelperComponents/clientTable'
  import repo from '../Repositories/manageProposal'
  import { util } from '../mixins/util'
  import appSelect from '../HelperComponents/select'
  import modal from '../HelperComponents/modal'

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
      appSelect,
      modal
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
              callBack: this.details
            },

            Manage: {
              callBack: this.manage,
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

        statusSelect: {
          data: [{ value: 1, text: "Accept" }, { value: 2, text: "Reject" }],
          label: 'Status'
        },

        selected: {value: '', text: ''},
        selectedType: { value: '', text: '' },

        detailsModal: false,

        proposal: {
          areaOfStudy: '',
          title: '',
          description: '',
          type: 1,
          status: 2,
          student: null
        },

        intern: {
          language: '',
          frameWorkDescription: '',
          internshipReason: '',
          isHaveInternship: false,
          companyName: '',
          companyAddress: '',
          jobDescriotion: '',
          internshipRefernce: '',
          contactForSupervisor: '',
          type: 2,
          status: 2,
          student: null
        },
        modalData: 0,

        manageModal: false,
      //  status: {value: 1}

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

      },
      details(row) {

        this.detailsModal = true;
        this.modalData = row.type;

        if (row.type == 3) {
          this.map(row, this.intern);
        } else {

          this.map(row, this.proposal);
        }       
      },

      manage() {

        this.manageModal = true;
      },

      confirmStatus() {

        repo.changeStatus()
      }
    }
  }


</script>

<style>

</style>
