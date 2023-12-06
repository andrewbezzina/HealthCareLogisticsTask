using FluentResults;

using MedicationRecords.Domain.Models;
using MedicationRecords.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;


namespace MedicationRecords.Application.Services.Implementations
{
    public class ActiveIngredientsService : IActiveIngredientsService
    {
        private readonly MedicationContext _context;

        public ActiveIngredientsService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<ActiveIngredients> GetOrCreateAsync(string activeIngredientList)
        {
            var result = await GetAsync(activeIngredientList);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(activeIngredientList)).Value;
        }

        public async Task<Result<ActiveIngredients>> GetAsync(string activeIngredientList)
        {
            var existingItem = await _context.ActiveIngredients.FirstOrDefaultAsync(a => a.ActiveIngredientList == activeIngredientList);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {activeIngredientList}"));
        }

        public async Task<Result<ActiveIngredients>> CreateAsync(string activeIngredientList)
        {

            var existingItem = await _context.ActiveIngredients.FirstOrDefaultAsync(a => a.ActiveIngredientList == activeIngredientList);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {activeIngredientList} already exists."));
            }

            var activeIngredient = new ActiveIngredients() { ActiveIngredientList = activeIngredientList };
            await _context.ActiveIngredients.AddAsync(activeIngredient);

            return activeIngredient;
        }

        public async Task<Result<ActiveIngredients>> CreateAndSaveAsync(string activeIngredientList)
        {
            var activeIngredient = await CreateAsync(activeIngredientList);
            await _context.SaveChangesAsync();
            return activeIngredient;
        }
    }
}
