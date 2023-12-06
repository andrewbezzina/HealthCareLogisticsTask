using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IClassificationService
    {
        Task<Result<Classification>> CreateAndSaveAsync(string classificationName);
        Task<Result<Classification>> CreateAsync(string classificationName);
        Task<Result<Classification>> GetAsync(int id);
        Task<Result<Classification>> GetAsync(string classificationName);
        Task<Classification> GetOrCreateAsync(string classificationName);
        Task<Result<Classification>> UpdateAndSaveAsync(int id, string name);
    }
}