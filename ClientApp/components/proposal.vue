<template>
  <div class="content">
    <div class="content-header">
      <h2>Proposals</h2>
    </div>

    <tab :tabs="tabNames">
      <div slot="Project">

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

      </div>


      <div slot="Internship">


      </div>

    </tab>

  </div>

</template>

<script>
  import tab from '../HelperComponents/Tab'
  import repo from '../Repositories/Proposals'

  export default {

    components: {
      tab
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

      submit(type,event) {


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
