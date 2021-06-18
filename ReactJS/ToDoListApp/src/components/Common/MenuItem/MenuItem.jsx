import React from "react";
import { MenuLink } from "./styles";

const MenuItem = (props) => {
  return (<MenuLink to={props.href}>{props.title}</MenuLink>);
}

export default MenuItem;