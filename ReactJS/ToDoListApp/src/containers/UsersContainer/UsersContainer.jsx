import React from "react";
import { BasicLayout } from "../../layouts";
import { UserService } from "./../../services";

class UsersContainer extends React.Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);

    this.state = {
      users: [],
      quantity: 10
    }
  }

  handleChange(event) {
    this.setState({ quantity: event.target.value });
  }

  componentDidMount() {
    UserService.getUsers(this.state.quantity).then(users => {
      this.setState({ users });
    });
  }

  componentDidUpdate(prevProps, prevState) {
    if(prevState.quantity !== this.state.quantity) {
      UserService.getUsers(this.state.quantity).then(users => {
        this.setState({ users });
      });
    }
  }

  getListUsers() {
    return (
      this.state.users.map((user, index) =>
        <div key={index}>{index+1}) { user.email}</div>
      ));
  }

  render() {
    return (
      <BasicLayout header="Users">
        <input type="number" className="form-control" value={this.state.quantity} onChange={this.handleChange} />
        {this.getListUsers()}
      </BasicLayout>);
  }
}

export default UsersContainer;