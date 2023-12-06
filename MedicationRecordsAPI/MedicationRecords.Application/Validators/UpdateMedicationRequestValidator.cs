using FluentValidation;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Domain.Enumerations;

namespace MedicationRecords.Application.Validators
{
    public class UpdateMedicationRequestValidator : AbstractValidator<UpdateMedicationRequest>
    {
        public UpdateMedicationRequestValidator()
        {
            RuleFor(m => m.Name)
                .Cascade(CascadeMode.Stop)
                .Length(3, 200)
                .When(m => m.Name != null);
            RuleFor(m => m.ActiveIngredients)
                .Cascade(CascadeMode.Stop)
                .Length(3, 300)
                .When(m => m.ActiveIngredients != null);
            RuleFor(m => m.PharmaceuticalForms)
                .Cascade(CascadeMode.Stop)
                .Length(3, 200)
                .When(m => m.PharmaceuticalForms != null);
            RuleFor(m => m.TherapeuticClass)
                .Cascade(CascadeMode.Stop)
                .Length(3, 100)
                .When(m => m.TherapeuticClass != null);
            RuleFor(m => m.ATCCode)
                .Cascade(CascadeMode.Stop)
                .Length(3, 10)
                .When(m => m.ATCCode != null);
            RuleFor(m => m.Classification)
               .Cascade(CascadeMode.Stop)
               .Length(2, 5)
               .When(m => m.Classification != null);
            RuleFor(m => m.Unit)
               .Cascade(CascadeMode.Stop)
               .Length(1, 20)
               .When(m => m.Unit != null);
            RuleFor(m => m.CompetentAuthorityStatus)
               .Cascade(CascadeMode.Stop)
               .IsEnumName(typeof(CompetentAuthorityStatus))
               .When(m => m.CompetentAuthorityStatus != null);
            RuleFor(m => m.InternalStatus)
               .Cascade(CascadeMode.Stop)
               .IsEnumName(typeof(InternalStatus))
               .When(m => m.InternalStatus != null);
        }

        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(char.IsLetter);
        }

    }
}
