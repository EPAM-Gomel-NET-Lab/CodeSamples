import React from "react";
import { BasicLayout } from "../../layouts";

class HomeContainer extends React.Component {

  getMessage() {
    const data = JSON.parse(localStorage.getItem("user"));
    const message = data ? `Welcome, ${data.name}!` : "Welcome!";
    return message;
  }

  render() {
    return (
      <BasicLayout>
        <h1>{this.getMessage()}</h1>
      </BasicLayout>
    )};
}

export default HomeContainer;