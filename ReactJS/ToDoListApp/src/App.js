import './App.css';
import {
  HomeContainer,
  TodoListContainer,
  UsersContainer,
  LoginContainer
} from "./containers";
import { NotFound } from "./components/Common";
import { Route, Switch, withRouter } from "react-router-dom";
import { routes } from "./constants";

const App = ({ history }) => {

  return (
    <Switch>
      <Route exact history={history} path={routes.home.href} component={HomeContainer} />
      <Route history={history} path={routes.login.href} component={LoginContainer} />
      <Route history={history} path={routes.users.href} component={UsersContainer} />
      <Route history={history} path={routes.toDoList.href} component={TodoListContainer} />
      <Route component={NotFound} />
    </Switch>
  );
}

export default withRouter(App);
