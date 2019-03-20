<template>
  <div class="content">
    <div class="row">
      <div class="col-md-2 my-1">
        <appSelect :config="semesterFilter" v-model="semester" @change="semesterFilterChange"></appSelect>
      </div>
      <div class="col-md-2 my-1">
        <appSelect :config="typeFilter" v-model="proposalType" @change="proposalFilterChange" ></appSelect>
      </div>
    </div>
    <appTable :tableConfig="table"></appTable>
  </div>
</template>

<script>

  import appTable from '../HelperComponents/clientTable'
  import roles from '../rolesConstant'
  import repo from '../Repositories/supervisorRepository'
  import { util } from '../mixins/util'
  import appSelect from '../HelperComponents/select'

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
      appSelect
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
              cssClass: 'btn-primary'
            },
            Marks: {
              cssClass: 'btn-info'
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
          student: null
        },

        proposal: {
          areaOfStudy: '',
          title: '',
          description: '',
          type: 1,
          status: 2,
          student: null
        },
        detailsModal: false,
        modalData: 0
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

        this.detailsModal = true;

        this.modalData = row.type;

        if (row.type == 3) {

          this.map(row, this.intern);

        } else {
          this.map(row, this.proposal);
        }
      }

    }

  }

</script>
<style>

</style>
