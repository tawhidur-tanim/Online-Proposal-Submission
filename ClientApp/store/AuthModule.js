import axios from 'axios'
import router from "../router/index"

const state = {

  token: null,

  userId: null,

  expireDate: null
}

const getters = {

  isAuthenticated(state) {

    return state.token != null;
  }

}

const mutations = {

  'authData'(state,authData) {

    state.token = authData.token;
    state.userId = authData.id;
    state.expireDate = authData.expireTime;
  },

  'clearAuth'(state) {

    state.token = null;
    state.userId = null;
    state.expireDate = null;
  }

}

const actions = {

  'logIn'({commit},authData) {

    let loader = this._vm.$loading.show({
      loader: 'spinner',
      color: '#0ACFE8'
    })
    axios.post('/api/account/login', authData)
      .then(({ data }) => {

        localStorage.setItem('token',data.token);
        localStorage.setItem('expireTime', data.expireTime);
        localStorage.setItem('userId', data.id);

        commit('authData', data)

        router.push('/');
        loader.hide()
      })
      .catch(() => {
        this._vm.$toastr.e('Wrong username or password');
        loader.hide()
      })

  },

  'logOut'({ commit }) {

    localStorage.removeItem('token');
    localStorage.removeItem('expireTime');
    localStorage.removeItem('userId');

    commit('clearAuth');

    router.replace('/login')
  },

  'tryLogIn'({ commit }) {

    const token = localStorage.getItem('token');

    if (!token) {
      return;
    }

    var expireIn = localStorage.getItem('expireTime')
    const expireDate = new Date(expireIn);
    const now = new Date();

    if (now >= expireDate) {
      return;
    }

    commit('authData',{
      id: localStorage.getItem('userId'),
      token: token,
      expireTime: expireIn
    })

  }
}


export default {
  state,
  getters,
  mutations,
  actions
}
