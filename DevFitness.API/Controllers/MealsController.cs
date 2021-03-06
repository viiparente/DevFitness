using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        /// <summary>
        /// Todas as refeições do User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(int userId)
        {

            var allMeals = _dbContext.Meals
                .Where(m => m.UserId == userId && m.Active);
            
            var allMealsViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));
            
            Log.Information("Método Get finalizado!");

            return Ok(allMealsViewModels);
        }

        //api/users/4/meals/16
        /// <summary>
        /// Uma refeição do User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mealId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Cadastro de Refeição para o User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Atualizar a refeição do User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mealId"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals
                .SingleOrDefault(m => m.UserId == userId && m.Id == mealId);
            if (meal == null)
                return NotFound();

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);
            
            _dbContext.SaveChanges();

            Log.Information("Método Put (Atualização de refeição) finalizado!");

            return NoContent();
        }

        //api/users/4/meals/16  HTTP DELETE
        /// <summary>
        /// Deletar a refeição do user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mealId"></param>
        /// <returns></returns>
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
