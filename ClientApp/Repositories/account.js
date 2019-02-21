import axios from 'axios'

export default {

  sendAuth(authData) {

    return axios.post('/api/account/login', authData).then((response) => {

      return response.data;
    });

  }


}
