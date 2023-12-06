using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IPharmaceuticalFormsService
    {
        Task<Result<PharmaceuticalForms>> CreateAndSaveAsync(string pharmaceuticalFormsList);
        Task<Result<PharmaceuticalForms>> CreateAsync(string pharmaceuticalFormsList);
        Task<Result<PharmaceuticalForms>> GetAsync(string pharmaceuticalFormsList);
        Task<PharmaceuticalForms> GetOrCreateAsync(string pharmaceuticalFormsList);
    }
}