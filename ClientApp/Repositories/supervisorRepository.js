import axios from 'axios'
import store from "../store/index"
import bus from "../HelperComponents/Bus"

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

  getSupervisorCategory(semesterId,studentId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/sup/category/${semesterId}/${studentId}/1`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  saveMarks(marks,teacherId) {

    store.commit('toggleLoader');

    const request = axios.post(`/api/user/saveMarks?teacherId=${teacherId}`,marks);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  passStudent(studentId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/passStudent/${studentId}`);

    request.catch((error) => {

      if (store.getters.isSpin) {
        store.commit('toggleLoader')
      }

      if (typeof error.response.data == "string") {

        bus.$emit("error", error.response.data);
      }

    }).then(() => {
      if (store.getters.isSpin) {
        store.commit('toggleLoader')
      }
    })

    return request;
  },

  getReviewerCategory(semesterId, studentId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/sup/category/${semesterId}/${studentId}/2`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  },

  getStudentsByReviewerr(id) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/user/teacher/students?teacherId=${id}&type=false`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;
  }
}
