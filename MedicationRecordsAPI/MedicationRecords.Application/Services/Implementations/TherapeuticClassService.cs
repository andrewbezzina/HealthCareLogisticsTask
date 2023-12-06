using FluentResults;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationRecords.Application.Services.Implementations
{
    public class TherapeuticClassService : ITherapeuticClassService
    {
        private readonly MedicationContext _context;

        public TherapeuticClassService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<TherapeuticClass> GetOrCreateAsync(string therapeuticclassName)
        {
            var result = await GetAsync(therapeuticclassName);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(therapeuticclassName)).Value;
        }

        public async Task<Result<TherapeuticClass>> GetAsync(string therapeuticClassName)
        {
            var existingItem = await _context.TherapeuticClasses.FirstOrDefaultAsync(a => a.TherapeuticClassName == therapeuticClassName);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {therapeuticClassName}"));
        }

        public async Task<Result<TherapeuticClass>> CreateAsync(string therapeuticClassName)
        {
            var existingItem = await _context.TherapeuticClasses.FirstOrDefaultAsync(a => a.TherapeuticClassName == therapeuticClassName);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {therapeuticClassName} already exists.")); ;
            }

            var therapeuticClass = new TherapeuticClass() { TherapeuticClassName = therapeuticClassName };
            await _context.TherapeuticClasses.AddAsync(therapeuticClass);

            return therapeuticClass;
        }

        public async Task<Result<TherapeuticClass>> CreateAndSaveAsync(string therapeuticClassName)
        {
            var therapeuticClass = await CreateAsync(therapeuticClassName);
            await _context.SaveChangesAsync();
            return therapeuticClass;
        }
    }
}
