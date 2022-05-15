import React from 'react';
import ChessBoard from './chessBoard.js';

export default class Game extends React.Component{

  constructor(props){
      super(props);
      this.state={selectedFigure: '', possibleMoves:[], status:''};
  }

  handleClick(i) {

    if (this.state.possibleMoves?.find(move => move.Row = i[0] && move.Column == i[1]))
    {
      fetch(process.env.REACT_APP_API+'ChessGameInterface/PlayMove/' + this.state.selectedFigure[0] + '-' + this.state.selectedFigure[1]
                                                                     + '--'
                                                                     + i[0] + '-' +  i[1])
      .then(response=>response.json())
      .then(data=>{
          this.setState({status: JSON.parse(data).toString()});
      })
    }
    else if(this.state.selectedFigure == '' || !this.state.possibleMoves?.find(move => move.Row = i[0] && move.Column == i[1]))
    {
      fetch(process.env.REACT_APP_API+'ChessGameInterface/GetPossibleMoves/' +  i[0] + '-' +  i[1])
      .then(response=>response.json())
      .then(data=>{
          this.setState({selectedFigure:i, possibleMoves:JSON.parse(data)});
      })
    }
    
  }

  render(){
    return(    
      <div>
        <h1>{this.state.status}</h1>
        <ChessBoard onClick={(i) => this.handleClick(i)}/>
      </div>
    );
  }

};
