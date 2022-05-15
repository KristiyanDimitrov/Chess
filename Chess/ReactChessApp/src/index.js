import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Game from './components/game.js';

if (module.hot) {
  module.hot.accept();
}

class App extends React.Component{
  constructor(props){
    super(props);

    this.state = {tst: "test"};
  }


  render(){
    return (
    <div>
      <Game />
    </div>);
  }
}

ReactDOM.render(<App />, document.querySelector("#root"));