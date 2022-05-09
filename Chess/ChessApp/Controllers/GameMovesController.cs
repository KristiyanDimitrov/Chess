using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ChessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameMovesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GameMovesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
