import axios from 'axios'

const axiosClient = axios.create({
  baseURL: 'https://6a485499abfcbaade1196167.mockapi.io',
  headers: {
    'Content-Type': 'application/json',
  },
})

export default axiosClient
