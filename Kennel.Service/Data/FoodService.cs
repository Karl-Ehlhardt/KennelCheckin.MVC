using Kennel.Data.Users;
using Kennel.Models.Data.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KennelData.Data;
using System.Web.Http;
using System.Data.Entity;

namespace Kennel.Service.Data
{
    public class FoodService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public FoodService(Guid userId)
        {
            _userId = userId;
        }

        //Create new
        public async Task<bool> CreateFood(FoodCreate model)
        {
            Food dogFood =
                new Food()
                {
                    Name = model.Name,
                    AmountPerMeal = model.AmountPerMeal,
                    MorningMeal = model.MorningMeal,
                    EveningMeal = model.EveningMeal
                };

            _context.Foods.Add(dogFood);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<FoodDetails> GetFoodById([FromUri] int id)
        {
            var query =
                await
                _context
                .Foods
                .Where(q => q.FoodId == id)
                .Select(
                    q =>
                    new FoodDetails()
                    {
                        Name = q.Name,
                        AmountPerMeal = q.AmountPerMeal,
                        MorningMeal = q.MorningMeal,
                        EveningMeal = q.EveningMeal
                    }).ToListAsync();
            return query[0];
        }

        //Get by id
        public async Task<FoodEdit> GetFoodByIdEditable([FromUri] int id)
        {
            var query =
                await
                _context
                .Foods
                .Where(q => q.FoodId == id)
                .Select(
                    q =>
                    new FoodEdit()
                    {
                        Name = q.Name,
                        AmountPerMeal = q.AmountPerMeal,
                        MorningMeal = q.MorningMeal,
                        EveningMeal = q.EveningMeal
                    }).ToListAsync();
            return query[0];
        }

        //Update by id
        public async Task<bool> UpdateFood([FromUri] int id, [FromBody] FoodEdit model)
        {
            Food dogFood =
                _context
                .Foods
                .Single(a => a.FoodId == id);
            dogFood.Name = model.Name;
            dogFood.AmountPerMeal = model.AmountPerMeal;
            dogFood.MorningMeal = model.MorningMeal;
            dogFood.EveningMeal = model.EveningMeal;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
