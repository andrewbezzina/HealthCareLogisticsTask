using FluentValidation;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Domain.Enumerations;

namespace MedicationRecords.Application.Validators
{
    public class CreateMedicationRequestValidator : AbstractValidator<CreateMedicationRequest>
    {
        public CreateMedicationRequestValidator()
        {
            RuleFor(m => m.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 200);
            RuleFor(m => m.ActiveIngredients)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 300);
            RuleFor(m => m.PharmaceuticalForms)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 200);
            RuleFor(m => m.TherapeuticClass)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 100);
            RuleFor(m => m.ATCCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 10);
            RuleFor(m => m.Classification)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(2, 5);
            RuleFor(m => m.Unit)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Length(1, 20);
            RuleFor(m => m.CompetentAuthorityStatus)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .IsEnumName(typeof(CompetentAuthorityStatus));
            RuleFor(m => m.InternalStatus)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .IsEnumName(typeof(InternalStatus));
        }

    }
}
