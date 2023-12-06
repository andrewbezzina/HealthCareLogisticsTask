using FluentResults;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IMedicationService
    {
        Task<Result<Medication>> CreateAsync(CreateMedicationRequest request);

        Task<Result<Medication>> CreateAndSaveAsync(CreateMedicationRequest request);
        Task<Result<Medication>> GetAsync(int id);
        Task<List<Medication>> GetAllAsync();
        Task<Result<Medication>> UpdateAsync(int id, UpdateMedicationRequest request);
        Task<Result> DeleteAsync(int id);
    }
}