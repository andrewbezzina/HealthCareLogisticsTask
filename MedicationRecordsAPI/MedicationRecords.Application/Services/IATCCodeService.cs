using FluentResults;
using MedicationRecords.Domain.Models;

namespace MedicationRecords.Application.Services
{
    public interface IATCCodeService
    {
        Task<Result<ATCCode>> CreateAndSaveAsync(string atcCodeName);
        Task<Result<ATCCode>> CreateAsync(string atcCodeName);
        Task<Result<ATCCode>> GetAsync(string atcCode);
        Task<ATCCode> GetOrCreateAsync(string atcCode);
    }
}