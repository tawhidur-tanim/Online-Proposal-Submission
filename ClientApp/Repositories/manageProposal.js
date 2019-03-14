import axios from 'axios'
import store from "../store/index"

export default {

  getProposals() {

    store.commit('toggleLoader');

    const request = axios.get("/api/proposal/GetProposals")

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  },


  changeStatus(id, statusId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/status/${id}/${statusId}`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  }
}
