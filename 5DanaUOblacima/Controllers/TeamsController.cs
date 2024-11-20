using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;
        public TeamsController(TeamService teamService)
        {
            this._teamService = teamService;
        }
        // GET: api/<TeamsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_teamService.GetTeams(null).Result);
        }

        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_teamService.GetTeams(id).Result);
        }

        // POST api/<TeamsController>
        [HttpPost]
        public IActionResult Post([FromBody] TeamInsertDTO dto)
        {
            bool result = _teamService.AddTeam(dto);
            if (result)
            {
                return Ok(_teamService.GetTeams(null).Result);
            }
            else
            {
                return StatusCode(409);
            }
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
