import axios from "axios";


const AxiosClient = axios.create({
    baseURL: process.env.REACT_APP_API_URL != null
    ? process.env.REACT_APP_API_URL : 'http://localhost:80',
});

export default AxiosClient;