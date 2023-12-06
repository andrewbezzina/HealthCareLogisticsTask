using FluentResults;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MedicationRecords.Application.Services;

namespace MedicationRecords.Application.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly MedicationContext _context;

        public UnitService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<Unit> GetOrCreateAsync(string unitName)
        {
            var result = await GetAsync(unitName);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(unitName)).Value;
        }

        public async Task<Result<Unit>> GetAsync(string unitName)
        {
            var existingItem = await _context.Units.FirstOrDefaultAsync(a => a.UnitName == unitName);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {unitName}"));
        }

        public async Task<Result<Unit>> CreateAsync(string unitName)
        {
            var existingItem = await _context.Units.FirstOrDefaultAsync(a => a.UnitName == unitName);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {unitName} already exists."));
            }

            var unit = new Unit() { UnitName = unitName };
            await _context.Units.AddAsync(unit);

            return unit;
        }

        public async Task<Result<Unit>> CreateAndSaveAsync(string unitName)
        {
            var unit = await CreateAsync(unitName);
            await _context.SaveChangesAsync();
            return unit;
        }

        public async Task<Result<Unit>> GetAsync(int id)
        {
            var existingItem = await _context.Units.FirstOrDefaultAsync(a => a.Id == id);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail("Not Found");
        }

        public async Task<Result<Unit>> UpdateAndSaveAsync(int id, string name)
        {
            var existingItem = await _context.Units.FirstOrDefaultAsync(a => a.Id == id);
            if (existingItem != null)
            {
                existingItem.UnitName = name;
                await _context.SaveChangesAsync();
                return existingItem;
            }

            return Result.Fail("Not Found");
        }
    }
}
