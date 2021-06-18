import React from "react";
import MenuItem from "./../MenuItem";

const Menu = (props) => {
  const items = props.items.map((item, index) => (
    <MenuItem
      key={index}
      href={item.href}
      title={item.title}
    />
  ));

  return (<div>{items}</div>);
}

export default Menu;