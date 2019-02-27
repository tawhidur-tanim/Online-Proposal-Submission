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
              <textarea class="form-control" v-model="proposal.areaOfStudy" v-validate="'required'" name="area"></textarea>
              <span class="error-font">{{errors.first('area')}}</span>
            </div>
            <div class="form-group">
              <label> Tentative Title of Project/ Thesis:</label>
              <textarea class="form-control" v-validate="'required'" name="title" v-model="proposal.title"></textarea>
              <span class="error-font">{{errors.first('title')}}</span>
            </div>
            <div class="form-group">
              <label>Brief Description About Proposal</label>
              <textarea class="form-control" v-validate="'required'" v-model="proposal.description" name="description" cols="5" rows="5"></textarea>
              <span class="error-font">{{errors.first('description')}}</span>
            </div>
            <div class="form-group">
              <label>Type: </label>
              <select class="form-control w25" v-model="proposal.type" v-validate="'required'" name="type">
                <option value="1">Project</option>
                <option value="3">Thesis</option>
              </select>
            </div>
            <button @click.prevent="submit(1,$event)" class="btn btn-success">Submit Proposal</button>

          </template>

          <template slot="Internship">

            <div class="form-group">
              <label>Any Programming Language or Database you are skilled?</label>
              <textarea class="form-control" name="language"></textarea>
            </div>
            <div class="form-group">
              <label>
                Any Framework you are familiar with? If YES, then write an overview of a project/task that you have developed with that framework.
              </label>
              <textarea name="frameDescription" class="form-control"></textarea>
            </div>
            <div class="form-group">
              <label>Why do you want to do Internship? Write a paragraph not more than 150 words</label>
              <textarea name="reason" cols="5" rows="5" class="form-control"></textarea>
            </div>

            <div class="form-group">
              <label>Do you have already managed any Internship/Job?</label>
              <select class="form-control w25" name="isHaveInternship">
                <option value="1">Yes</option>
                <option value="0">No</option>
              </select>
            </div>
            <div class="form-group">
              <label>Company Name:</label>
              <input type="text" name="companyName" class="form-control w25" />
            </div>
            <div class="form-group">
              <label>Company Address: </label>
              <textarea class="form-control" name="companyAddress"></textarea>
            </div>

            <div class="form-group">
              <label>Your Job Responsibility: </label>
              <textarea class="form-control" name="jobDescriotion"></textarea>
            </div>

            <div class="form-group">
              <label>How did you get this Internship/Job? Any reference or contact person regarding this? </label>
              <textarea class="form-control" name="internshipRefernce"></textarea>
            </div>

            <div class="form-group">
              <label>Contact Info of your Line Manager or Supervisor: </label>
              <textarea class="form-control" name="ContactForSupervisor"></textarea>
            </div>



          </template>

        </tab>
      </template>
    </box>



  </div>

</template>

<script>
  import tab from '../HelperComponents/Tab'
  import repo from '../Repositories/Proposals'
  import box from '../HelperComponents/box'

  export default {

    components: {
      tab,
      box
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

        types: {

          1: 'Project',
          3: 'Thesis',
          2: 'Internship'
        }

      }
    },

    methods: {

      submit(type, event) {


        this.$validator.validateAll().then((result) => {

          if (!result) {
            return;
          }

          repo.saveProposal(this.proposal).then((data) => {

            console.log("proposal Data", data);

            this.$toastr.s("Success");

          })


        })


      }
    }
  }

</script>
