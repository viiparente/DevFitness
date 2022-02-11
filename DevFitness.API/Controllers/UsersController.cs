using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //api/users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        //api/users - método HTTP POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            return CreatedAtAction(nameof(Get), new { id = 1 }, inputModel);
        }

        //api/users/1 - método HTTP PUT 
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            return NoContent();
        }
    }


}
