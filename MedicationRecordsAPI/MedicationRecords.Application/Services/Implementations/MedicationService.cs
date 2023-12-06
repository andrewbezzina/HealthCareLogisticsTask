using FluentResults;
using FluentValidation;
using MedicationRecords.Application.Services;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Domain.Enumerations;
using MedicationRecords.Domain.Models;
using MedicationRecords.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace MedicationRecords.Application.Services.Implementations
{

    public class MedicationService : IMedicationService
    {
        private readonly MedicationContext _context;
        private readonly IActiveIngredientsService _activeIngredientsService;
        private readonly IPharmaceuticalFormsService _pharmaceuticalFormsService;
        private readonly ITherapeuticClassService _therapeuticClassService;
        private readonly IATCCodeService _atcCodeService;
        private readonly IClassificationService _classificationService;
        private readonly IUnitService _unitService;
        private readonly IValidator<CreateMedicationRequest> _createMedicationRequestValidator;
        private readonly IValidator<UpdateMedicationRequest> _updateMedicationRequestValidator;

        public MedicationService(MedicationContext context,
                                 IActiveIngredientsService activeingredientService,
                                 IPharmaceuticalFormsService pharmaceuticalFormsService,
                                 ITherapeuticClassService therapeuticClassService,
                                 IATCCodeService atcCodeService,
                                 IClassificationService classificationService,
                                 IUnitService unitService,
                                 IValidator<CreateMedicationRequest> createMedicationRequestValidator,
                                 IValidator<UpdateMedicationRequest> updateMedicationRequestValidator)
        {
            _context = context;
            _activeIngredientsService = activeingredientService;
            _pharmaceuticalFormsService = pharmaceuticalFormsService;
            _therapeuticClassService = therapeuticClassService;
            _atcCodeService = atcCodeService;
            _classificationService = classificationService;
            _unitService = unitService;
            _createMedicationRequestValidator = createMedicationRequestValidator;
            _updateMedicationRequestValidator = updateMedicationRequestValidator;
        }

        public async Task<Result<Medication>> CreateAsync(CreateMedicationRequest request)
        {
            var validationResult = _createMedicationRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.ToString());
            }

            if (await MedicationNameExistsInDatabaseAsync(request.Name))
            {
                return Result.Fail($"Medication with Name: {request.Name} already exist in Database");
            }

            try
            {
                var medication = await CreateMedicationFromRequestAsync(request);

                await _context.Medications.AddAsync(medication.Value);

                return medication;
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result<Medication>> CreateAndSaveAsync(CreateMedicationRequest request)
        {
            var result = await CreateAsync(request);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Result<Medication>> GetAsync(int id)
        {
            if (!await MedicationIdExistsInDatabaseAsync(id))
            {
                return Result.Fail($"No Medication found for Id: {id}");
            }

            var medication = await _context.Medications
                .Include(m => m.ActiveIngredients)
                .Include(m => m.ATCCode)
                .Include(m => m.Classification)
                .Include(m => m.PharmaceuticalForms)
                .Include(m => m.TherapeuticClass)
                .Include(m => m.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);

            return medication;
        }

        public async Task<List<Medication>> GetAllAsync()
        {
            var medications = await _context.Medications
                .Include(m => m.ActiveIngredients)
                .Include(m => m.ATCCode)
                .Include(m => m.Classification)
                .Include(m => m.PharmaceuticalForms)
                .Include(m => m.TherapeuticClass)
                .Include(m => m.Unit)
                .ToListAsync();

            return medications;
        }

        public async Task<Result<Medication>> UpdateAsync(int id, UpdateMedicationRequest request)
        {
            var validationResult = _updateMedicationRequestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.Fail(validationResult.ToString());
            }

            var result = await GetAsync(id);
            if (result.IsFailed)
            {
                return result;
            }

            try
            {
                var medication = result.Value;

                if (!request.Name.IsNullOrEmpty())
                {
                    if (medication.Name != request.Name && await MedicationNameExistsInDatabaseAsync(request.Name))
                    {
                        return Result.Fail($"Name: {request.Name} already used by another medication");
                    }
                    medication.Name = request.Name;
                }

                if (!request.ActiveIngredients.IsNullOrEmpty()) medication.ActiveIngredients = await _activeIngredientsService.GetOrCreateAsync(request.ActiveIngredients);
                if (!request.PharmaceuticalForms.IsNullOrEmpty()) medication.PharmaceuticalForms = await _pharmaceuticalFormsService.GetOrCreateAsync(request.PharmaceuticalForms);
                if (!request.TherapeuticClass.IsNullOrEmpty()) medication.TherapeuticClass = await _therapeuticClassService.GetOrCreateAsync(request.TherapeuticClass);
                if (!request.ATCCode.IsNullOrEmpty()) medication.ATCCode = await _atcCodeService.GetOrCreateAsync(request.ATCCode);
                if (!request.Classification.IsNullOrEmpty()) medication.Classification = await _classificationService.GetOrCreateAsync(request.Classification);
                if (!request.CompetentAuthorityStatus.IsNullOrEmpty()) medication.CompetentAuthorityStatus = Enum.Parse<CompetentAuthorityStatus>(request.CompetentAuthorityStatus);
                if (!request.InternalStatus.IsNullOrEmpty()) medication.InternalStatus = Enum.Parse<InternalStatus>(request.InternalStatus);
                if (!request.Unit.IsNullOrEmpty()) medication.Unit = await _unitService.GetOrCreateAsync(request.Unit);

                await _context.SaveChangesAsync();

                return medication;
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var result = await GetAsync(id);
            if (result.IsFailed)
            {
                return result.ToResult();
            }

            try
            {
                _context.Remove(result.Value);
                await _context.SaveChangesAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task<Result<Medication>> CreateMedicationFromRequestAsync(CreateMedicationRequest request)
        {
            return new Medication()
            {
                Name = request.Name,
                ActiveIngredients = await _activeIngredientsService.GetOrCreateAsync(request.ActiveIngredients),
                PharmaceuticalForms = await _pharmaceuticalFormsService.GetOrCreateAsync(request.PharmaceuticalForms),
                TherapeuticClass = await _therapeuticClassService.GetOrCreateAsync(request.TherapeuticClass),
                ATCCode = await _atcCodeService.GetOrCreateAsync(request.ATCCode),
                Classification = await _classificationService.GetOrCreateAsync(request.Classification),
                CompetentAuthorityStatus = Enum.Parse<CompetentAuthorityStatus>(request.CompetentAuthorityStatus),
                InternalStatus = Enum.Parse<InternalStatus>(request.InternalStatus),
                Unit = await _unitService.GetOrCreateAsync(request.Unit)
            };
        }

        private async Task<bool> MedicationNameExistsInDatabaseAsync(string name)
        {
            return await _context.Medications.AnyAsync(m => m.Name == name);
        }

        private async Task<bool> MedicationIdExistsInDatabaseAsync(int id)
        {
            return await _context.Medications.AnyAsync(m => m.Id == id);
        }
    }
}
