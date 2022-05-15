using Chess.Figures.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ChessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChessGameInterfaceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private Chess.Game _curentGame;
        public ChessGameInterfaceController(IConfiguration configuration)
        {
            _configuration = configuration;
            _curentGame = new();
        }

        [HttpGet]
        public JsonResult Get()
        {
            var result = new JsonResult(JsonConvert.SerializeObject(_curentGame.Chessboard.Figures));

            return result;
        }

        [HttpPut]
        public JsonResult Put(string pos)
        {


            return new JsonResult("Updated Successfully");
        }


        [Route("GetPossibleMoves/{row}-{col}")]
        public JsonResult GetPossibleMoves(int row, int col)
        {
            Position fromPos = new Position(row, col);
            var possibleMoves = _curentGame.GetPossibleMoves(fromPos);

            return new JsonResult(JsonConvert.SerializeObject(possibleMoves));
        }

        [Route("PlayMove/{fromRow}-{fromCol}--{toRow}-{toCol}")]
        public JsonResult PlayMove(int fromRow, int fromCol, int toRow, int toCol)
        {
            Position fromPos = new Position(fromRow, fromCol);
            Position toPos = new Position(toRow, toCol);

            _curentGame.PlayMove(fromPos, toPos);

            return new JsonResult(JsonConvert.SerializeObject("Figure moved."));
        }


        }
       
}

