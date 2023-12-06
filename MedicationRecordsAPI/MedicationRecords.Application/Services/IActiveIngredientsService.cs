using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IActiveIngredientsService
    {
        Task<Result<ActiveIngredients>> CreateAndSaveAsync(string activeIngredientList);
        Task<Result<ActiveIngredients>> CreateAsync(string activeIngredientList);
        Task<Result<ActiveIngredients>> GetAsync(string activeIngredientList);
        Task<ActiveIngredients> GetOrCreateAsync(string activeIngredientList);
    }
}