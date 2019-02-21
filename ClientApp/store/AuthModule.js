import axios from 'axios'
import router from '../router/index'
import account from '../Repositories/account'

const state = {

  token: null,

  userId: null,

  expireDate: null,

  roles: []
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
  },

  role(state) {

    return state.roles[0];
  }

}

const mutations = {

  'authData'(state,authData) {

    state.token = authData.token;
    state.userId = authData.id;
    state.expireDate = authData.expireTime;
    state.roles = authData.roles;
  },

  'clearAuth'(state) {

    state.token = null;
    state.userId = null;
    state.expireDate = null;
    state.roles = null;
  }

}

const actions = {

  'logIn'({commit, dispatch},authData) {

    commit('showLoader');

    account.sendAuth(authData)
      .then(( data ) => {

        localStorage.setItem('token',data.token);
        localStorage.setItem('expireTime', data.expireTime);
        localStorage.setItem('userId', data.id);
        localStorage.setItem('roles', JSON.stringify(data.roles));

        commit('authData', data);

        dispatch('tryLogout');

        this._vm.$http.defaults.headers.common['Authorization'] = "bearer "+data.token;
        axios.defaults.headers.common['Authorization'] = "bearer "+data.token;

        router.push('/');
        commit('hideLoader');

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
    localStorage.removeItem('roles');

    commit('clearAuth');
    delete this._vm.$http.defaults.headers.common['Authorization'];
    delete axios.defaults.headers.common['Authorization'];
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
      expireTime: expireIn,
      roles: JSON.parse(localStorage.getItem('roles'))
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
