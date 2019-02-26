import axios from 'axios'

export default {


  getSemesters() {

    return axios.get("/api/semester/semesters").then(({ data }) => {

      console.log("from repo");

      return data;
    })

  },

  saveProposal(proposal) {

    return axios.post("/api/proposal/create", proposal).then(({ data }) => data);
  }


}
