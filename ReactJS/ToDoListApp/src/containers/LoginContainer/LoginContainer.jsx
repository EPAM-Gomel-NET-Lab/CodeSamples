import React from "react";
import { routes } from "./../../constants";
import { UserService } from "./../../services";

class LoginContainer extends React.Component {
  constructor(props){
    super(props);

    this.state = {
      userName: "",
      password: ""
    };
  }

  userNameHandler = (event) => {
    this.setState({ userName: event.target.value });
  }

  passwordHandler = (event) => {
    this.setState({ password: event.target.value });
  }

  formHandler = (event) => {
    event.preventDefault();
    const userName = this.state.userName;
    const password = this.state.password;
    
    if(userName && password) {
      UserService.login(userName, password).then(result => {
        if (result) {
          this.props.history.push(routes.home.href);
        }

        this.setState({ userName: "", password: "" });
      });
    }
  }

  render() {
    return (
      <div className="container">
        <form onSubmit={this.formHandler}>
          <div class="form-group m-2">
            <label>User Name</label>
            <input type="text" className="form-control" value={this.state.userName} onChange={this.userNameHandler} />
          </div>
          <div class="form-group m-2">
            <label>Password</label>
            <input type="text" className="form-control" value={this.state.password} onChange={this.passwordHandler} />
          </div>
          <div className="m-2">
            <button type="submit" className="btn btn-primary">Login</button>
          </div>
        </form>
      </div>
    );
  }
}

export default LoginContainer;