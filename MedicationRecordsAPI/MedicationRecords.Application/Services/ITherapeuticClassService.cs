using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface ITherapeuticClassService
    {
        Task<Result<TherapeuticClass>> CreateAndSaveAsync(string therapeuticClassName);
        Task<Result<TherapeuticClass>> CreateAsync(string therapeuticClassName);
        Task<Result<TherapeuticClass>> GetAsync(string therapeuticClassName);
        Task<TherapeuticClass> GetOrCreateAsync(string therapeuticclassName);
    }
}