import React from 'react';
import Square from './square.js';

export default class ChessBoard extends React.Component{

  constructor(props){
      super(props);
      this.state={board:[]};
  }

  refresh(){
    fetch(process.env.REACT_APP_API+'ChessGameInterface')
    .then(response=>response.json())
    .then(data=>{
        this.setState({board:JSON.parse(data)});
    })};

  renderSquare(i, squareShade, style){
    return (<Square
        key={i}
        keyVal={i}
        style={require('./chess-king2.jpg')}
        shade={squareShade}
        onClick={() => this.props.onClick(i)}
      />)
    };
 

  componentDidMount(){
      this.refresh();
    };


  render(){
    const builtBoard = [];
    let rowcnt = 0

    this.state.board.forEach(row => {
      const squareRows = [];
      let colcnt = 0;
      
      row.forEach(figure =>
        {
          const squareShade = ( (isEven(rowcnt) && isEven(colcnt)) || (!isEven(rowcnt) && !isEven(colcnt)) ? "light-square" : "dark-square");
          const key = rowcnt.toString() + colcnt.toString();
  
          squareRows.push(this.renderSquare(key, squareShade));

          colcnt = colcnt + 1;
        });

      if(squareRows.length > 0)
      {
        builtBoard.push(<div className="board-row" key={rowcnt}>{squareRows}</div>)
      };

      rowcnt = rowcnt + 1;     
    });


    return(    
      <div>
        {builtBoard}
      </div>
    );
  }

};


function isEven(num) {
  return num % 2 === 0
}