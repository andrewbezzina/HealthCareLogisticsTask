using FluentResults;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MedicationRecords.Application.Services;


namespace MedicationRecords.Application.Services.Implementations
{
    public class PharmaceuticalFormsService : IPharmaceuticalFormsService
    {
        private readonly MedicationContext _context;

        public PharmaceuticalFormsService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<PharmaceuticalForms> GetOrCreateAsync(string pharmaceuticalFormsList)
        {
            var result = await GetAsync(pharmaceuticalFormsList);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(pharmaceuticalFormsList)).Value;
        }

        public async Task<Result<PharmaceuticalForms>> GetAsync(string pharmaceuticalFormsList)
        {
            var existingItem = await _context.PharmaceuticalForms.FirstOrDefaultAsync(a => a.PharmaceuticalFormsList == pharmaceuticalFormsList);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {pharmaceuticalFormsList}"));
        }

        public async Task<Result<PharmaceuticalForms>> CreateAsync(string pharmaceuticalFormsList)
        {
            var existingItem = await _context.PharmaceuticalForms.FirstOrDefaultAsync(a => a.PharmaceuticalFormsList == pharmaceuticalFormsList);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {pharmaceuticalFormsList} already exists.")); ;
            }

            var pharmaceuticalForms = new PharmaceuticalForms() { PharmaceuticalFormsList = pharmaceuticalFormsList };
            await _context.PharmaceuticalForms.AddAsync(pharmaceuticalForms);

            return pharmaceuticalForms;
        }

        public async Task<Result<PharmaceuticalForms>> CreateAndSaveAsync(string pharmaceuticalFormsList)
        {
            var pharmaceuticalForms = await CreateAsync(pharmaceuticalFormsList);
            await _context.SaveChangesAsync();
            return pharmaceuticalForms;
        }
    }
}
