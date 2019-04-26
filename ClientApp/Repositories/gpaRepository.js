import axios from 'axios'
import store from "../store/index"


export default {


  getCourses(studentId) {

    store.commit('toggleLoader');

    const request = axios.get(`/api/course/getcourses/${studentId}`);

    request.catch(() => {

      if (store.getters.isSpin) {
        store.commit('toggleLoader');
      }

    }).then(() => {
      if (store.getters.isSpin) {
        store.commit('toggleLoader');
      }
    });

    return request;
  },

  saveGpa(gpas) {

    store.commit('toggleLoader');

    const request = axios.post(`/api/course/SaveStudentGpa`, gpas);

    request.catch(() => {

      if (store.getters.isSpin) {
        store.commit('toggleLoader');
      }

    }).then(() => {
      if (store.getters.isSpin) {
        store.commit('toggleLoader');
      }
    });

    return request;
  }


}
