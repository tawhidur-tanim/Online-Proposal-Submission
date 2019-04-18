import axios from 'axios'
import store from "../store/index"


export default {

  getDash() {

    store.commit('toggleLoader');

    const request = axios.get(`/api/semester/dashboard`);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  },

  passChange(resource) {

    store.commit('toggleLoader');

    const request = axios.post(`/api/account/PasswordChange`,resource);

    request.catch().then(() => store.commit('toggleLoader'))

    return request;

  }


} 
