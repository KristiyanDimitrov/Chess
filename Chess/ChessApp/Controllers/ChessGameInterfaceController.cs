using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
suing s

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


        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {


            return new JsonResult("test");
        }

        public void GameStart()
        {
            Chess.Game currentGame = new();

            //while (!currentGame.GameEnded)
            //{
            //    Print.PrintBoard(currentGame.Chessboard);

            //    //Validates position is valid and it contains a figure that corresponds to the current player color
            //    Position from = Print.GetPositionFrom_User(currentGame);

            //    //Get possible moves
            //    List<Position> possibleMoves = currentGame.GetPossibleMoves(from);
            //    if (!possibleMoves.Any())
            //        continue;

            //    Console.Clear();
            //    Print.PrintBoard(currentGame.Chessboard, possibleMoves);

            //    //Validates it is a valid position
            //    Position to = Print.GetPositionTo_User(currentGame, possibleMoves);

            //    //Play the selected move
            //    if (to == null)
            //        continue;

            //    currentGame.PlayMove(from, to);
            }
        }
       
}

