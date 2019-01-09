import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import Login from 'components/login'
import NavBar from 'components/nav-new'
import store from "../store/index"

export const routes = [

  { name: 'home', path: '/', components: {
    default: HomePage,
    nav: NavBar
  },
    beforeEnter(to, from, next) {

      console.log(store.getters.isAuthenticated, store.state.Auth.token)

      if (store.getters.isAuthenticated) {
       next()
     } else {
       next('/login')
     }
   },
    display: 'Home', icon: 'home'
  },

  {
    name: 'counter', path: '/counter', components: {
      default: CounterExample,
      nav: NavBar
    },
    beforeEnter(to, from, next) {
      if (store.getters.isAuthenticated) {
        next()
      } else {
        next('/login')
      }
    },
    display: 'Counter', icon: 'graduation-cap'
  },

  {
    name: 'fetch-data', path: '/fetch-data', components: {
      default: FetchData,
      nav: NavBar
    },
    //beforeEnter(to, from, next) {
    //  console.log('Routes ', to, from);
    //  if (store.getters.isAuthenticated) {
    //    next()
    //  } else {
    //    next('/login')
    //  }
    //},
    display: 'Fetch data', icon: 'list'
  },

  { name: 'login', path: '/login', component: Login, display: 'Fetch data', icon: 'list' }
]
