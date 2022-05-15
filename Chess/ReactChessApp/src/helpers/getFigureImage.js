export default function getFigureImage(figure) {
    let imgLocation = '';
  
    switch(figure) {
        case "P":
            imgLocation = './chess-king2.jpg'
          break;
        case "B":
            imgLocation = './chess-king2.jpg'
          break;
        case "K":
            imgLocation = './chess-king2.jpg'
          break;
        case "N":
            imgLocation = './chess-king2.jpg'
          break;
        case "Q":
            imgLocation = './chess-king2.jpg'
          break;
        case "R":
            imgLocation = './chess-king2.jpg'
          break;
        default:
            imgLocation = ''
    }
  
    return imgLocation;
  }