using ExcelDataReader;
using MedicationRecords.Infrastructure.DataAccess;
using MedicationRecords.Contracts.Requests;
using FluentResults;
using MedicationRecords.Application.Services;

namespace MedicationRecords.Application.Services.Implementations
{
    public class DataInitializationService : IDataInitializationService
    {
        private enum excelIndexes
        {
            ActiveIngredients = 0,
            PharmaceuticalForms,
            TherapeuthicClasses,
            ATCCode,
            Name,
            Classification,
            CompetentAuthorityStatus,
            InternalStatus,
            Unit
        }

        private const string XLSX_FILE_PATH = "Medications.xlsx";
        private readonly MedicationContext _context;
        private readonly IMedicationService _medicationService;

        public DataInitializationService(MedicationContext context, IMedicationService medicationService)
        {
            _context = context;
            _medicationService = medicationService;
        }
        public async Task<Result> LoadInitialDataAsync()
        {
            if (DatabaseHasData())
            {
                return Result.Fail("Database Already Populated");
            }

            return await ReadDateFromExcelFileAndSaveToDb();
        }

        private bool DatabaseHasData()
        {
            return _context.Medications.Count() != 0;
        }

        private async Task<Result> ReadDateFromExcelFileAndSaveToDb()
        {
            try
            {
                using (var stream = File.Open(XLSX_FILE_PATH, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // skip header
                        reader.Read();

                        while (reader.Read())
                        {
                            CreateMedicationRequest request = new()
                            {
                                Name = reader.GetString((int)excelIndexes.Name),
                                ActiveIngredients = reader.GetString((int)excelIndexes.ActiveIngredients),
                                PharmaceuticalForms = reader.GetString((int)excelIndexes.PharmaceuticalForms),
                                TherapeuticClass = reader.GetString((int)excelIndexes.TherapeuthicClasses),
                                ATCCode = reader.GetString((int)excelIndexes.ATCCode),
                                Classification = reader.GetString((int)excelIndexes.Classification),
                                CompetentAuthorityStatus = reader.GetString((int)excelIndexes.CompetentAuthorityStatus),
                                InternalStatus = reader.GetString((int)excelIndexes.InternalStatus),
                                Unit = reader.GetString((int)excelIndexes.Unit)
                            };

                            await _medicationService.CreateAndSaveAsync(request);
                        }

                        await _context.SaveChangesAsync();
                    }
                }
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
