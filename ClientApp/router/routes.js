import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import Login from 'components/login'
import NavBar from 'components/nav-new'
import semester from 'components/semester'
import proposal from 'components/proposal'
import manageProp from 'components/manageProposal'
import supervisor from 'components/supervisor'
import reviewer from 'components/reviewer'
import settings from 'components/settings'

export const routes = [

  { name: 'home', path: '/', components: {
    default: HomePage,
    nav: NavBar
  }, 
    display: 'Home', icon: 'home'
  },

  {
    name: 'counter', path: '/counter', components: {
      default: CounterExample,
      nav: NavBar
    },
    display: 'Counter', icon: 'graduation-cap'
  },

  {
    name: 'fetch-data', path: '/fetch-data', components: {
      default: FetchData,
      nav: NavBar
    },
    display: 'Fetch data', icon: 'list'
  },
  {
    name: 'semesters', path: '/semesters', components: {
      default: semester,
      nav: NavBar
    }
  },
  {
    name: 'proposals', path: '/proposals', components: {
      default: proposal,
      nav: NavBar
    }
  },
  {
    name: 'manageProposal', path: '/manageProposal', components: {
      default: manageProp,
      nav: NavBar
    }
  },
  {
    name: 'supervisor', path: '/supervisor', components: {
      default: supervisor,
      nav: NavBar
    }
  },
  {
    name: 'reviewer', path: '/reviewer', components: {
      default: reviewer,
      nav: NavBar
    }
  },
  {
    name: 'settings', path: '/settings', components: {
      default: settings,
      nav: NavBar
    }
  },
  { name: 'login', path: '/login', component: Login, display: 'Fetch data', icon: 'list' },
  { path: '*', redirect: '/' }
]

