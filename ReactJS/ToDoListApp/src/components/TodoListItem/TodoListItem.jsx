import React from "react";
import './TodoListItem.css';
import { BookmarkCheck } from 'react-bootstrap-icons';

const TodoListItem = (props) => {
  const onClickClose = () => {
    const index = parseInt(props.index);
    props.removeItem(index);
  }

  const onClickDone = () => {
    const index = parseInt(props.index);
    props.markTodoDone(index);
  }

  const todoClass = props.item.done ? "done" : "undone";

  return (
    <li className="list-group-item ">
      <div className={todoClass}>
        <BookmarkCheck onClick={onClickDone} className="check"/>
        {props.item.value}
        <button type="button" className="close" onClick={onClickClose}>&times;</button>
      </div>
    </li>
  );
}

export default TodoListItem;