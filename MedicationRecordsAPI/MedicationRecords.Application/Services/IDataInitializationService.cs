using FluentResults;

namespace MedicationRecords.Application.Services
{
    public interface IDataInitializationService
    {
        Task<Result> LoadInitialDataAsync();
    }
}