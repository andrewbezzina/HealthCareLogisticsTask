using FluentResults;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationRecords.Application.Services.Implementations
{
    public class ClassificationService : IClassificationService
    {
        private readonly MedicationContext _context;

        public ClassificationService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<Classification> GetOrCreateAsync(string classificationName)
        {
            var result = await GetAsync(classificationName);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(classificationName)).Value;
        }

        public async Task<Result<Classification>> GetAsync(string classificationName)
        {
            var existingItem = await _context.Classifications.FirstOrDefaultAsync(a => a.ClassificationName == classificationName);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {classificationName}"));
        }

        public async Task<Result<Classification>> CreateAsync(string classificationName)
        {
            var existingItem = await _context.Classifications.FirstOrDefaultAsync(a => a.ClassificationName == classificationName);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {classificationName} already exists."));
            }

            var classification = new Classification() { ClassificationName = classificationName };
            await _context.Classifications.AddAsync(classification);

            return classification;
        }

        public async Task<Result<Classification>> CreateAndSaveAsync(string classificationName)
        {
            var classification = await CreateAsync(classificationName);
            await _context.SaveChangesAsync();
            return classification;
        }

        public async Task<Result<Classification>> GetAsync(int id)
        {
            var existingItem = await _context.Classifications.FirstOrDefaultAsync(a => a.Id == id);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail("Not Found");
        }

        public async Task<Result<Classification>> UpdateAndSaveAsync(int id, string name)
        {
            var existingItem = await _context.Classifications.FirstOrDefaultAsync(a => a.Id == id);
            if (existingItem != null)
            {
                existingItem.ClassificationName = name;
                await _context.SaveChangesAsync();
                return existingItem;
            }

            return Result.Fail("Not Found");
        }
    }
}
