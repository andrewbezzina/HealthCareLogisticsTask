using FluentResults;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationRecords.Application.Services.Implementations
{
    public class ATCCodeService : IATCCodeService
    {
        private readonly MedicationContext _context;

        public ATCCodeService(MedicationContext context)
        {
            _context = context;
        }

        public async Task<ATCCode> GetOrCreateAsync(string atcCode)
        {
            var result = await GetAsync(atcCode);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            return (await CreateAsync(atcCode)).Value;
        }

        public async Task<Result<ATCCode>> GetAsync(string atcCode)
        {
            var existingItem = await _context.ATCCodes.FirstOrDefaultAsync(a => a.Code == atcCode);
            if (existingItem != null)
            {
                return existingItem;
            }

            return Result.Fail(new Error($"No record found mathcing name: {atcCode}"));
        }
        public async Task<Result<ATCCode>> CreateAsync(string atcCodeName)
        {
            var existingItem = await _context.ATCCodes.FirstOrDefaultAsync(a => a.Code == atcCodeName);
            if (existingItem != null)
            {
                return Result.Fail(new Error($"Record with matching name: {atcCodeName} already exists."));
            }

            var atcCode = new ATCCode() { Code = atcCodeName };
            await _context.ATCCodes.AddAsync(atcCode);

            return atcCode;
        }

        public async Task<Result<ATCCode>> CreateAndSaveAsync(string atcCodeName)
        {
            var atcCode = await CreateAsync(atcCodeName);
            await _context.SaveChangesAsync();
            return atcCode;
        }
    }
}
