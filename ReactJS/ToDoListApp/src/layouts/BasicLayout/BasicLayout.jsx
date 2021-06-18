import React from "react";
import { Header, Menu } from "../../components/Common";
import { routes } from "./../../constants";

export const BasicLayout = ({ children, ...props }) => (
  <div className="container">
    <Menu items={Object.values(routes)} />
    <Header title={props.header} />
    {children}
  </div>
);
