using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IUnitService
    {
        Task<Result<Unit>> CreateAndSaveAsync(string unitName);
        Task<Result<Unit>> CreateAsync(string unitName);
        Task<Result<Unit>> GetAsync(int id);
        Task<Result<Unit>> GetAsync(string unitName);
        Task<Unit> GetOrCreateAsync(string unitName);
        Task<Result<Unit>> UpdateAndSaveAsync(int id, string name);
    }
}