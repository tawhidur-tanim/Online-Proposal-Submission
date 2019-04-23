import axios from 'axios'
import store from "../store/index"


export default {


  saveTeachers(file) {

    const form = new FormData();
    store.commit('toggleLoader');


    form.append("teacherList", file);

    const request = axios.post("/api/user/SaveTeacher", form, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });

    request.catch(() => {

      if (store.getters.isSpin) {
        store.commit('toggleLoader')
      }

    }).then(() => {
      if (store.getters.isSpin) {
        store.commit('toggleLoader')
      }
    })

    return request;
  }
}
