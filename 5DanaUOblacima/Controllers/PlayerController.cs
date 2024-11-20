using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Services;
using DataAcess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;
        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _playerService.GetPlayer(id).Result;
            if (result != null) return Ok(result);
            else return StatusCode(404);

        }

        // POST api/<PlayerController>
        [HttpPost]
        public IActionResult Post([FromBody] PlayerInsertDTO dto)
        {
           bool result=_playerService.AddPlayer(dto);
            if (result)
            {
                return Ok(_playerService.GetPlayer(null).Result);
            }
            else
            {
                return StatusCode(409);
            }
            
        }

        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
