import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import { ClientTable } from 'vue-tables-2'

let options = {};
let useVuex = false;
let theme = "bootstrap3";
let template = "default";

Vue.use(ClientTable, options, useVuex, theme, template);
// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.prototype.$http = axios

import Toastr from 'vue-toastr';
// import toastr scss file: need webpack sass-loader
require('vue-toastr/src/vue-toastr.scss');
// Register plugin
Vue.use(Toastr, { /* options */ });

sync(store, router)


const app = new Vue({
  store,
  router,
  render: h => h(App)
})

export {
  app,
  router,
  store
}
