using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        //api/users/4/meals
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            return Ok();
        }

        //api/users/4/meals/16
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            return Ok();
        }

        //api/users/4/meals
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = 1 }, inputModel); // redireciona para essa url
        }

        //api/users/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            return NoContent();
        }

        //api/users/4/meals/16  HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            return NoContent();
        }
    }
}
