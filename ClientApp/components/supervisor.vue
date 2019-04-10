<template>
  <div class="content">
    <div class="row">
      <div class="col-md-2 my-1">
        <appSelect :config="semesterFilter" v-model="semester" @change="semesterFilterChange"></appSelect>
      </div>
      <div class="col-md-2 my-1">
        <appSelect :config="typeFilter" v-model="proposalType" @change="proposalFilterChange"></appSelect>
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

    <modal v-if="marksModal"  @close="marksModal = false" cls="md">
      <template slot="header">
        <h3>Students Marks</h3>
      </template>

      <template slot="body">

        <div class="row mb10" v-for="cat in marksCategory">
          <div class="col-md-2">
           <label> {{ cat.name}}: </label>
          </div>
          <div class="col-md-3">
            <input :name="cat.name" class="form-control"/>
          </div>
        </div>

      </template>

      <template slot="footer">
        <button class="btn btn-success">Save Marks</button>
      </template>
    </modal>
  </div>
</template>

<script>

  import appTable from '../HelperComponents/clientTable'
  import roles from '../rolesConstant'
  import repo from '../Repositories/supervisorRepository'
  import { util } from '../mixins/util'
  import appSelect from '../HelperComponents/select'
  import modal from '../HelperComponents/modal'

  export default {
    mixins: [util],
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

      repo.getSemesterFilters().then(({ data}) => {

        this.semesterFilter.data = data;
        this.semesterFilter.data.push({ value: -1, text: 'All' });
      })

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
          filterable: ["fullName"],
          columns: ["fullName", "type"],
          templates: {

            fullName(row,h) {

              return h("span", row.fullName + "\n [" + row.userName + "]");
            },

            type: (row,h) => {

              if (row.proposals.length > 0) {

                var proposalType = row.proposals[0].type;
                return h('label', { 'class': 'label label-success' }, this.types[proposalType]);

              }

              return "";

            }

          },
          actions: {

            Details: {
              cssClass: 'btn-primary',
              callBack: this.details
            },
            Marks: {
              cssClass: 'btn-info',
              callBack: this.supMarks
            },
            Release: {
              cssClass: 'btn-success'
            }

          },
          customFilters: [
            {
              name: 'type',
              callback: (row,query) => {

                if (query == -1) return true;
                if (row.proposals.length == 0) return false;

                return row.proposals[0].type == query; 
              }
            },
            {
              name: 'semester',
              callback: (row, query) => {

                if (query == -1) return true;
                
                return row.semesterId == query;
              }
            }
          ]
        },

        typeFilter: {
          data: [{ value: -1, text: 'All' },{ value: 1, text: 'Project' }, { value: 3, text: 'Thesis' }],
          label: "Proposal Type"
        },
        semesterFilter: {
          data: [],
          label: "Semesters"
        },
        proposalType: { value: -1, text: '' },

        semester: { value: -1 },

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
          student: {}
        },

        proposal: {
          areaOfStudy: '',
          title: '',
          description: '',
          type: 1,
          status: 2,
          student: {}
        },
        detailsModal: false,
        modalData: 0,

        marksCategory: [],

        marksModal: false
      }
    },

    methods: {

      proposalFilterChange() {

        this.$tableEvent.$emit('vue-tables.filter::type', this.proposalType.value);
      },

      semesterFilterChange() {
        this.$tableEvent.$emit('vue-tables.filter::semester', this.semester.value);

      },
      details(row) {

        this.detailsModal = row.proposals.length > 0;

        this.modalData = row.proposals[0].type;

        if (row.proposals[0].type == 3) {

          this.map(row.proposals[0], this.intern);
          this.map(row, this.intern.student);

        } else {
          this.map(row.proposals[0], this.proposal);
          this.proposal.student = this.__copy(row);

        }
      },

      supMarks(row) {

        repo.getSupervisorCategory(row.semesterId).then(({ data }) => {

          this.marksCategory = data;
          this.marksModal = true;
        })

      }

    }

  }

</script>
<style>

</style>
