import { user, token } from "./../constants";
import jwt_decode from "jwt-decode";

const axios = require('axios').default;

class UserService {

  async login(userName, password) {
    if (userName === user.userName && password === user.password) {
      const data = jwt_decode(token);
      localStorage.setItem("token", token);
      localStorage.setItem("user", JSON.stringify(data));

      return true;
    }

    return false;
  }

  async getUsers(quantity) {
    try {
      const result = await axios.get(`https://randomuser.me/api/?results=${quantity}`);
      return result.data.results;
    } catch (error) {
      console.log(error);
      return [];
    }
  }
}

export default new UserService();