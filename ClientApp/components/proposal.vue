<template>
  <div class="content">
    <div class="content-header">
      <h2>Proposals</h2>
    </div>

    <box>
      <template slot="title">
        Proposal Submission Form
      </template>
      <template slot="body">
        <tab :tabs="tabNames">
          <template slot="Project">
            <div class="form-group">
              <label>Area Of Study: </label>
              <textarea class="form-control" v-model="proposal.areaOfStudy" v-validate="'required'" name="area" data-vv-scope="project" ></textarea>
              <span class="error-font">{{errors.first('project.area')}}</span>
            </div>
            <div class="form-group">
              <label> Tentative Title of Project/ Thesis:</label>
              <textarea class="form-control" v-validate="'required'" name="title" v-model="proposal.title" data-vv-scope="project" ></textarea>
              <span class="error-font">{{errors.first('project.title')}}</span>
            </div>
            <div class="form-group">
              <label>Brief Description About Proposal</label>
              <textarea class="form-control" v-validate="'required'" v-model="proposal.description" name="description" cols="5" rows="5" data-vv-scope="project" ></textarea>
              <span class="error-font">{{errors.first('project.description')}}</span>
            </div>
            <div class="form-group">
              <label>Type: </label>
              <select class="form-control w25" v-model="proposal.type" v-validate="'required'" name="type" data-vv-scope="project" >
                <option value="1">Project</option>
                <option value="3">Thesis</option>
              </select>
            </div>
            <button @click.prevent="submit(1,$event)" class="btn btn-success">Submit Proposal</button>

          </template>

          <template slot="Internship">

            <div class="form-group">
              <label>Any Programming Language or Database you are skilled?</label>
              <textarea class="form-control" name="language" v-validate="'required'" data-vv-scope="intern" v-model="intern.language"></textarea>
              <span class="error-font">{{ errors.first('intern.language') }}</span>
            </div>

            <div class="form-group">
              <label>
                Any Framework you are familiar with? If YES, then write an overview of a project/task that you have developed with that framework.
              </label>
              <textarea name="frameDescription" class="form-control" v-validate="'required'" data-vv-scope="intern" v-model="intern.frameWorkDescription"></textarea>
              <span class="error-font">{{ errors.first('intern.frameDescription') }}</span>
            </div>

            <div class="form-group">
              <label>Why do you want to do Internship? Write a paragraph not more than 150 words</label>
              <textarea name="reason" cols="5" rows="5" class="form-control" v-validate="'required'" data-vv-scope="intern" v-model="intern.internshipReason" ></textarea>
              <span class="error-font" >{{ errors.first('intern.reason') }}</span>
            </div>

            <div class="form-group">
              <label>Do you have already managed any Internship/Job?</label>
              <select class="form-control w25" name="isHaveInternship" v-validate="'required'" data-vv-scope="intern" v-model="intern.isHaveInternship">
                <option :value="true">Yes</option>
                <option :value="false">No</option>
              </select>
              <span class="error-font" >{{ errors.first('intern.isHaveInternship') }}</span>
            </div>

            <template v-if="intern.isHaveInternship">
              <div class="form-group">
                <label>Company Name:</label>
                <input type="text" name="companyName" class="form-control w25" v-validate="'required'" data-vv-scope="intern" v-model="intern.companyName" />
                <span class="error-font" >{{ errors.first('intern.companyName') }}</span>
              </div>
              <div class="form-group">
                <label>Company Address: </label>
                <textarea class="form-control" name="companyAddress" v-validate="'required'" data-vv-scope="intern" v-model="intern.companyAddress"></textarea>
                <span class="error-font" >{{ errors.first('intern.companyAddress') }}</span>
              </div>

              <div class="form-group">
                <label>Your Job Responsibility: </label>
                <textarea class="form-control" name="jobDescriotion" v-validate="'required'" data-vv-scope="intern" v-model="intern.jobDescriotion" ></textarea>
                <span class="error-font" >{{ errors.first('intern.jobDescriotion') }}</span>
              </div>

              <div class="form-group">
                <label>How did you get this Internship/Job? Any reference or contact person regarding this? </label>
                <textarea class="form-control" name="internshipRefernce" v-validate="'required'" data-vv-scope="intern" v-model="intern.internshipRefernce" ></textarea>
                <span class="error-font" >{{ errors.first('intern.internshipRefernce') }}</span>
              </div>

              <div class="form-group">
                <label>Contact Info of your Line Manager or Supervisor: </label>
                <textarea class="form-control" name="contactForSupervisor" v-validate="'required'" data-vv-scope="intern" v-model="intern.contactForSupervisor"></textarea>
                <span class="error-font" >{{ errors.first('intern.contactForSupervisor') }}</span>
              </div>
            </template>

           

            <button class="btn btn-success" @click="submitIntership">Submit Proposal</button>

          </template>

        </tab>
      </template>
    </box>

    <apptable :tableConfig="tableConfig"></apptable>

  </div>

</template>

<script>
  import tab from '../HelperComponents/Tab'
  import repo from '../Repositories/Proposals'
  import box from '../HelperComponents/box'
  import table from '../HelperComponents/clientTable'
  import { util } from '../mixins/util'

  export default {

    mixins: [util],

    created() {

      repo.getOwnPorposals().then(({ data }) => {

        console.log(data);
        this.tableConfig.data = data;

      })


    },

    components: {
      tab,
      box,
      apptable : table
    },

    data() {

      return {

        tabNames: ['Project', 'Internship'],

        proposal: {

          areaOfStudy: '',
          title: '',
          description: '',
          type: 1,
          status: 2
        },

        intern: {

          language: '',
          FrameWorkDescription: '',
          internshipReason: '',
          isHaveInternship: false,
          companyName: '',
          companyAddress: '',
          jobDescriotion: '',
          internshipRefernce: '',
          contactForSupervisor: '',
          type: 2,
          status: 2
        },
        types: {

          1: 'Project',
          3: 'Thesis',
          2: 'Internship'
        },

        tableConfig: {
          data: [],

          columns: ["title","status","type"],
          templates: {

            status: function (row, h) {

              return row.status == 1
                ? h('label', { 'class': 'label label-success' }, "Accepted")
                : row.status == 2
                  ? h('label', { 'class': 'label label-primary' }, "Pending")
                  : h('label', { 'class': 'label label-danger' }, "Rejected");
            },

            type: function (row,h) {

              return row.type == 1
                ? h('label', { 'class': 'label label-success' }, "Project")
                : row.type == 2
                  ? h('label', { 'class': 'label label-success' }, "Internship")
                  : h('label', { 'class': 'label label-success' }, "Thesis");
            }

          },

          actions: {}
        }

      }
    },

    methods: {

      submit() {

        this.$validator.validateAll('project').then((result) => {

          if (!result) {
            return;
          }

          this.toggleLoader()
          repo.saveProposal(this.proposal).then(({ data}) => {

            console.log("proposal Data", data);

            this.$toastr.s("Success");
          })
            .catch((er) => {

              this.error(er);

            })
            .then(() => {

              this.toggleLoader()
            })

        })


      },

      submitIntership() {

        this.$validator.validateAll('intern').then((result) => {

          if (!result) {
            return;
          }

          this.toggleLoader()

          repo.saveProposal(this.intern).then((data) => {

            console.log("proposal Data", data);

            this.$toastr.s("Success");
          })
            .catch((er) => {

              this.error(er);

            })
            .then(() => {

              this.toggleLoader()
            })

        })

      }
    }
  }

</script>
