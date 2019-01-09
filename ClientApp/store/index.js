import Vue from 'vue'
import Vuex from 'vuex'
import Auth from "./AuthModule"
Vue.use(Vuex)

// TYPES
const MAIN_SET_COUNTER = 'MAIN_SET_COUNTER'

// STATE
const state = {
  counter: 1,

  toast: {
    success: false,
    error: false
  }
}

// MUTATIONS
const mutations = {
  [MAIN_SET_COUNTER] (state, obj) {
    state.counter = obj.counter
  }
}

// ACTIONS
const actions = ({
  setCounter ({ commit }, obj) {
    commit(MAIN_SET_COUNTER, obj)
  }
})

export default new Vuex.Store({
  state,
  mutations,
  actions,

  modules: {
    Auth
  }
})
