import Vue from 'vue'
import axios from 'axios'
import router from './router/index'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import { ClientTable, ServerTable, Event } from 'vue-tables-2'
import Toastr from 'vue-toastr'
import VueLoader from 'vue-loading-overlay'
import VeeValidate from 'vee-validate';
import 'vue-loading-overlay/dist/vue-loading.css'

let options = {};
let useVuex = false;
let theme = "bootstrap3";
let template = "default";

Vue.use(VeeValidate)
Vue.use(VueLoader);
Vue.use(ClientTable, options, useVuex, theme, template)
Vue.use(ServerTable, [options = {}], [useVuex = false], [theme = 'bootstrap3'], [template = 'default']);
Vue.use(require('vue-moment'))
Vue.component('icon', FontAwesomeIcon)
Vue.directive('click-outside', {
  bind: function (el, binding, vNode) {
    // Provided expression must evaluate to a function.
    if (typeof binding.value !== 'function') {
      const compName = vNode.context.name
      var warn = '[Vue-click-outside:] provided expression ' + binding.expression + ' is not a function, but has to be'
      if (compName) {
        warn += 'Found in component ' + compName
      }

      console.warn(warn)
    }
    // Define Handler and cache it on the element
    const bubble = binding.modifiers.bubble
    const handler = function (e) {
      if (bubble || (!el.contains(e.target) && el !== e.target)) {
        binding.value(e)
      }
    }
    el.__vueClickOutside__ = handler

    // add Event Listeners
    document.addEventListener('click', handler)
  },

  unbind: function (el, binding) {
    // Remove Event Listeners
    document.removeEventListener('click', el.__vueClickOutside__);
    el.__vueClickOutside__ = null
  }
})

axios.interceptors.request.use(config => {
  //console.log('Request Interceptor', config)
  return config
})

axios.interceptors.response.use(res => {
 // console.log('Response Interceptor', res)
  return res
})
axios.defaults.headers.get['Accepts'] = 'application/json'
Vue.prototype.$http = axios

Vue.prototype.$tableEvent = Event;


require('vue-toastr/src/vue-toastr.scss')

Vue.use(Toastr, { /* options */ })

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
