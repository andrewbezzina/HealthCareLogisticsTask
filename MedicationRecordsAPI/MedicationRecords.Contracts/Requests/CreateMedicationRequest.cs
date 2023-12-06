namespace MedicationRecords.Contracts.Requests
{
    public record struct CreateMedicationRequest(string Name, string ActiveIngredients, string PharmaceuticalForms, string TherapeuticClass, string ATCCode, string Classification, string CompetentAuthorityStatus, string InternalStatus, string Unit);
}
