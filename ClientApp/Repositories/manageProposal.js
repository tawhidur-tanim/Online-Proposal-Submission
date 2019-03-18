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

    const request = axios.get(`/api/proposal/status/${id}/${statusId}`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  },


  assignTeacher(data) {

    store.commit('toggleLoader');

    const request = axios.post("/api/user/assign", data);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  },

  seminarAttendance(studentId, status) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/seminar?studentId=${studentId}&status=${status}`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  }

}
