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
  },

  saveCourses(file) {

    const form = new FormData();
    store.commit('toggleLoader');


    form.append("courses", file);

    const request = axios.post("/api/course/SaveCourses", form, {
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
  },


  getAllCourses() {

    store.commit('toggleLoader');

    const request = axios.get("/api/course/getall");

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
  },

  deleteCourse(id) {

    store.commit('toggleLoader');

    const request = axios.delete(`/api/course/delete/${id}`);

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
  },

  addCourse(course) {

    
    store.commit('toggleLoader');

    const request = axios.post(`/api/course/create/`,course);

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
