using ChessApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameMoveController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GameMoveController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT MoveID, GameID, PlayerID, Position FROM dbo.GameMoves";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ChessAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post( GameMove move)
        {
            string query = @"INSERT INTO dbo.GameMoves
                                (MoveID, GameID, PlayerID, Position)
                                values
                                (
                                    " + move.MoveID + @"'
                                    ,'" + move.GameID + @"'
                                    ,'" + move.PlayerID + @"'
                                    ,'" + move.Position + @"'
                                )";


            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ChessAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
