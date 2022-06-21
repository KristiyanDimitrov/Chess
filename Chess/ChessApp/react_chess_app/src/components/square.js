import React from 'react';


export default function Square(props) {

  return (
    <button className={"square " + props.shade}
      onClick={props.onClick}
      style={{backgroundImage: 'url(' + props.style + ')',  padding: "30px 30px", borderColor: "grey"}}
      key={props.keyVal}>
    </button>
  );

}
