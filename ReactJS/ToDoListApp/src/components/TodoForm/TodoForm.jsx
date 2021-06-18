import React from "react";
import "./TodoForm.css";

class TodoForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: ''};
    this.handleChange = this.handleChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  onSubmit(event) {
    event.preventDefault();
    const newItemValue = this.state.value;
    
    if(newItemValue) {
      this.props.addItem({ newItemValue });
      this.setState({ value: '' });
    }
  }

  render() {
    return (
      <form onSubmit={this.onSubmit} className="form-inline">
        <input type="text" className="form-control" placeholder="Add a new todo..." value={this.state.value} onChange={this.handleChange} />
        <button type="submit" className="btn btn-primary">Add</button>
      </form>
    );   
  }
}

export default TodoForm;