﻿using _5DanaUOblacima.DTO.Post;
using _5DanaUOblacima.Services;
using DataAcess;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _playerService;
        public PlayersController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/<PlayerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_playerService.GetPlayer(null).Result);
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if(id==null)  return StatusCode(404);
            var result = _playerService.GetPlayer(id).Result;
            if (result != null) return Ok(result);
            else return StatusCode(404);

        }

        // POST api/<PlayerController>
        [HttpPost("create")]
        public IActionResult Post([FromBody] PlayerInsertDTO dto)
        {
          _playerService.AddPlayer(dto);

          return Ok(_playerService.GetPlayer(null).Result);
            
           
            
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
