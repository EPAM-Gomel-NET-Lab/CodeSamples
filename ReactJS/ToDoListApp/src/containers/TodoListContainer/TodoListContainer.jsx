import React from "react";
import TodoList from "../../components/TodoList";
import TodoForm from "../../components/TodoForm";
import { BasicLayout } from "../../layouts";

class TodoListContainer extends React.Component {
  constructor (props) {
    super(props);
    this.addItem = this.addItem.bind(this);
    this.removeItem = this.removeItem.bind(this);
    this.markTodoDone = this.markTodoDone.bind(this);
    this.state = { todoItems: [] };
  }

  addItem(todoItem) {
    const { todoItems } = this.state;
    todoItems.unshift({
      index: todoItems.length + 1, 
      value: todoItem.newItemValue, 
      done: false
    });
    this.setState({ todoItems });
  }

  removeItem (itemIndex) {
    const { todoItems } = this.state;
    todoItems.splice(itemIndex, 1);
    this.setState({ todoItems });
  }

  markTodoDone(itemIndex) {
    const { todoItems } = this.state;
    const todo = todoItems[itemIndex];
    todoItems.splice(itemIndex, 1);
    todo.done = !todo.done;
    todo.done ? todoItems.push(todo) : todoItems.unshift(todo);
    this.setState({ todoItems });  
  }

  render() {
    return (
      <BasicLayout header="TO DO LIST">
        <TodoForm addItem={this.addItem} />
        <TodoList items={this.state.todoItems} removeItem={this.removeItem} markTodoDone={this.markTodoDone}/>
      </BasicLayout>
    );
  }
}

export default TodoListContainer;