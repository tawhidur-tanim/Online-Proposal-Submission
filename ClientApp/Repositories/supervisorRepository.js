import axios from 'axios'
import store from "../store/index"

export default {

  getStudentsBySupervisor(id) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/teacher/students?teacherId=${id}&type=true`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  getSemesterFilters() {

    store.commit('toggleLoader');

    const request = axios.get(`/api/semester/filters`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  getSupervisorCategory(semesterId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/sup/category/${semesterId}`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  }

}
