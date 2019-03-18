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
        <div class="col-md-2 my-1">
          <appSelect :config="selectSeminar" v-model="seminarAllow" @change="changeSelectSeminar"></appSelect>
        </div>
      </div>
      <appTable :tableConfig="table"></appTable>
      <modal v-if="detailsModal" @close="detailsModal = false" cls="md">
        <template slot="header">
          <h3>Proposal Details</h3>
        </template>
        <template slot="body">
          <div class="row">
            <div class="col-md-3">
              <label>Student Name</label>
            </div>
            <div class="col-md-3">
              <span v-if="proposal.student">{{ proposal.student.fullName }}</span>
              <span v-else>{{ intern.student.fullName }}</span>
            </div>
          </div>
          <div class="row">
            <div class="col-md-3">
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
      <modal v-if="manageModalShow" @close="manageModalShow = false" :cls="manageClass">
        <template slot="header">
          <h3>Manage</h3>
        </template>
        <template slot="body">
          <template v-if="manageModal.status.value != 1">
            <appSelect :config="statusSelect" v-model="manageModal.status"></appSelect>
            <button @click="confirmStatus" class="btn btn-primary">Confirm</button>
          </template>
          <template v-if="manageModal.status.value === 1">
            <template v-if="Object.keys(manageModal.supervisor).length > 0">
              <div class="row">
                <label class="col-md-3">
                  Supervisor Name:
                </label>
                <div class="col-md-2">
                  {{ manageModal.supervisor.fullName }}
                </div>
              </div>
              <div class="row">
                <label class="col-md-3">
                  Supervisor Id:
                </label>
                <div class="col-md-3">
                  {{ manageModal.supervisor.userName }}
                </div>
              </div>
            </template>

            <template v-if="Object.keys(manageModal.reviewer).length > 0">
              <div class="row">
                <label class="col-md-3">
                  Reviewer Name:
                </label>
                <div class="col-md-2">
                  {{ manageModal.reviewer.fullName }}
                </div>
              </div>
              <div class="row">
                <label class="col-md-3">
                  Reviewer Id:
                </label>
                <div class="col-md-3">
                  {{ manageModal.reviewer.userName }}
                </div>
              </div>
            </template>

            <hr />
            <div class="row" style="margin-bottom: 10px">
              <div class="col-md-6">
                <autoComplete source="/api/user/sups?query="
                              input-class="form-control" placeholder="Search supervisor"
                              :results-display="formattedDisplay"
                              @selected="addDistributionGroup" @clear="clear" :request-headers="auth">
                </autoComplete>
              </div>
              <div class="col-md-6">
                <button class="btn btn-success" @click="assign">Assign</button>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6">
                <autoComplete source="/api/user/sups?query="
                              input-class="form-control" placeholder="Search reviewer"
                              :results-display="formattedDisplay"
                              @selected="setReviewer" @clear="clearReviewer" :request-headers="auth">
                </autoComplete>
              </div>
              <div class="col-md-6">
                <button class="btn btn-success" @click="assignSuperVisor">Assign</button>
              </div>
            </div>
          </template>
        </template>
      </modal>

      <modal v-if="seminarShow" @close="seminarShow = false">
        <template slot="header">
          <h3>Seminar Attendance</h3>
        </template>

        <template slot="body">
          <appSelect :config="seminarOption" v-model="seminarAttendance" ></appSelect>
        </template>
        <template slot="footer">
          <button class="btn btn-success" @click="saveAttendance">Save</button>
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
  import autoComplete from 'vuejs-auto-complete'


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
      this.select.data.unshift({ value: '', text: 'All' })
      this.selectType.data.unshift({ value: '', text: 'All' })

    },

    components: {
      appTable,
      appSelect,
      modal,
      autoComplete
    },

    data() {

      return {
        table: {
          data: [],
          filterable: ["studentName"],
          columns: ["studentName", "status", "type", "supervisorName", "reviewerName"],
          templates: {

            status: (row, h) => {

              return h('label', { 'class': 'label label-' + this.color[row.status] }, this.status[row.status])
            },

            type: (row, h) => {

              return h('label', { 'class': 'label label-success' }, this.types[row.type]);
            }

          },

          actions: {

            Details: {
              callBack: this.details
            },

            Manage: {
              callBack: this.manage,
              cssClass: 'btn btn-primary'
            },

            Seminar: {

              callBack: this.seminarModal,
              cssClass: 'btn btn-info'

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
            },
            {
              name: 'seminar',
              callback: function (row, query) {

                if (query == -1) return true;

                if (!row.student || !row.student.isSeminar)
                  return false;


                return row.student.isSeminar == query;
              }
            }]
        },

        select: {
          data: [],
          label: "Status"
        },

        seminarShow: false,

        selectType: {
          data: [],
          label: "Type"
        },
        selectSeminar: {
          data: [{ value: -1, text: 'All' }, { value: true, text: 'Allowed' }],
          label: "Pre-Defence"
        },

        seminarAllow: {value: -1},
        seminarOption: {
          data: [{ value: true, text: 'Present' }, { value: false, text: 'Absent' }, { value: null, text: 'None' }],
          label: "Attendance"
        },
        statusSelect: {
          data: [{ value: 1, text: "Accept" }, { value: 2, text: "Pending" }, { value: 3, text: "Reject" }],
          label: 'Status'
        },

        selected: { value: '', text: '' },
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

        manageModal: {

          student: {},

          supervisor: {},

          reviewer: {},

          status: { value: 0, text: '' },

          id: ''

        },
        modalData: 0,

        manageModalShow: false,

        selectedObject: {},

        seminarAttendance: {value: null}
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
            row.supervisor = {}
          }

          if (row.reviewer && row.reviewer.fullName) {
            row.reviewerName = row.reviewer.fullName + "\n [" + row.reviewer.userName + "]";

          } else {
            row.reviewerName = "No Name Found";
            row.reviewer = {}
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
      changeSelectSeminar() {

        this.$tableEvent.$emit('vue-tables.filter::seminar', this.seminarAllow.value);

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

      manage(row) {

        this.manageModalShow = true;

        this.map(row, this.manageModal);

        this.manageModal.status = { value: row.status };

      },

      confirmStatus() {

        repo.changeStatus(this.manageModal.id, this.manageModal.status.value).then(() => {

          this.$toastr.s("Success");

          this.updateRow({ id: this.manageModal.id, status: this.manageModal.status.value })

        })

      },

      updateRow(newRow) {

        var row = this.table.data.find(x => x.id === newRow.id);

        this.map(newRow, row);
      },

      formattedDisplay(result) {

        return result.fullName + ' [' + result.userName + ']';
      },

      addDistributionGroup(arg) {

        this.manageModal.selectedObject = arg.selectedObject;
      },

      clear() {

        this.manageModal.selectedObject = {};

      },

      assign() {

        repo.assignTeacher({

          teacherId: this.manageModal.selectedObject.id,
          studentId: this.manageModal.student.id,
          type: 1
        }).then(() => {

          this.$toastr.s("Assigned");

          this.manageModal.supervisor = this.manageModal.selectedObject;

          this.updateRow({ supervisor: this.manageModal.selectedObject, id: this.manageModal.id })

          this.mapper(this.table.data);
        })

      },

      assignSuperVisor() {

        repo.assignTeacher({

          teacherId: this.manageModal.selectedReviewer.id,
          studentId: this.manageModal.student.id,
          type: 2
        }).then(() => {

          this.$toastr.s("Assigned");

          this.manageModal.reviewer = this.manageModal.selectedReviewer;

          this.updateRow({ reviewer: this.manageModal.selectedReviewer, id: this.manageModal.id })

          this.mapper(this.table.data);
        })

      },
      setReviewer(arg) {

        this.manageModal.selectedReviewer = arg.selectedObject;
        
      },
      clearReviewer() {

        this.manageModal.selectedReviewer = {}
      },

      seminarModal(row) {

        this.seminarShow = true
        this.map(row, this.manageModal);

        if (row.student.isSeminar != null) {
          this.seminarAttendance.value = row.student.isSeminar;
        }
      },
      saveAttendance() {

        if (this.seminarAttendance.value == null) {
          this.$toastr.i("Please Select absent or present");
          return;
        }

        repo
          .seminarAttendance(this.manageModal.student.id, this.seminarAttendance.value)
          .then(() => {

            this.$toastr.s("Updated");

            this.manageModal.student.isSeminar = this.seminarAttendance.value;
            this.seminarShow = false;
            this.updateRow({ id: this.manageModal.id, student: this.manageModal.student });

          })
      }
    },

    computed: {

      manageClass() {

        return {

          'md': this.manageModal.status.value == 1
        }
      },

      auth() {

        return {

          "Authorization": "bearer " + this.$store.getters.getToken
        }
      }
    }
  }


</script>
<style>
</style>
