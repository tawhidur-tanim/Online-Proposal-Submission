import axios from 'axios'
import store from "../store/index"

export default {


  getSemesters() {

    return axios.get("/api/semester/semesters").then(({ data }) => {

      console.log("from repo");

      return data;
    })

  },

  saveProposal(proposal) {

    return axios.post("/api/proposal/create", proposal)//.then(({ data }) => data);
  },


  getOwnPorposals() {

    store.commit('toggleLoader');

    const request = axios.get("/api/proposal/Own");

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  saveCv(cv) {

    store.commit('toggleLoader');

    const request = axios.post("/api/proposal/cv", cv);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  },

  getForm() {

    store.commit('toggleLoader');

    const request = axios.get("/api/proposal/truth")

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  }


}
