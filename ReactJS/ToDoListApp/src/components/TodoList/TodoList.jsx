import React from "react";
import TodoListItem from "./../TodoListItem";

const TodoList = (props) => {
  const items = props.items.map((item, index) => (
    <TodoListItem
      key={index}
      item={item}
      index={index}
      removeItem={props.removeItem}
      markTodoDone={props.markTodoDone}
    />
  ));

  return (<ul className="list-group"> {items} </ul>);
}


export default TodoList;