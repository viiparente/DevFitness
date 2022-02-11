using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;
        public MealsController(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //api/users/4/meals
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals
                .Where(m => m.UserId == userId && m.Active);
            
            var allMealsViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));

            return Ok(allMealsViewModels);
        }

        //api/users/4/meals/16
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            var meal = _dbContext.Meals
                .SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            
            if (meal == null)
                return NotFound();

            var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.Date);

            return Ok(mealViewModel);
        }

        //api/users/4/meals
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            var meal = new Meal(inputModel.Descripton, inputModel.Calories, inputModel.Date, userId);
            //var meal = _mapper.Map<Meal>(inputModel);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId, mealId = meal.Id }, inputModel); // redireciona para essa url
        }

        //api/users/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals
                .SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            if (meal == null)
                return NotFound();

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);
            
            _dbContext.SaveChanges();

            return NoContent();
        }

        //api/users/4/meals/16  HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals
                .SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            
            if (meal == null)
                return NotFound();

            meal.Deactivate();

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
