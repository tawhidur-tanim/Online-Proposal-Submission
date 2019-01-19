import axios from 'axios'
import router from '../router/index'

const state = {

  token: null,

  userId: null,

  expireDate: null
}

const getters = {

  isAuthenticated(state) {

    return state.token != null;
  },

  getToken(state) {

    return state.token;
  },

  getState(state) {

    return state;
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

  'logIn'({commit, dispatch},authData) {

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
        dispatch('tryLogout');

        this._vm.$http.defaults.headers.common['Authorization'] = "bearer "+data.token;

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
    delete this._vm.$http.defaults.headers.common['Authorization'];
    router.replace('/login')
  },

  'tryLogIn'({ commit, dispatch }) {

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

    this._vm.$http.defaults.headers.common['Authorization'] = "bearer " + token;


    commit('authData',{
      id: localStorage.getItem('userId'),
      token: token,
      expireTime: expireIn
    })

    dispatch('tryLogout');
  },

  'tryLogout'({ dispatch }) {

    var expireDate = new Date(localStorage.getItem('expireTime'));
    var miliSeconds = this._vm.$moment(expireDate).diff(new Date(), 'seconds') * 1000;

    setTimeout(function() {
      dispatch('logOut');
    }, miliSeconds)
  }
}


export default {
  state,
  getters,
  mutations,
  actions
}
