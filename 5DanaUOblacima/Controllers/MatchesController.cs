using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchService _matchService;
        public MatchesController(MatchService service)
        {
            _matchService= service;
        }
        // GET: api/<MatchesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MatchesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MatchesController>
        [HttpPost]
        public IActionResult Post([FromBody] MatchInsertDTO dto)
        {
            _matchService.AddMatch(dto);
            return Ok();
        }

        // PUT api/<MatchesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MatchesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
