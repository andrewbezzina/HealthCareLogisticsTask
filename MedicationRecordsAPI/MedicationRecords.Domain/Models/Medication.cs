using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MedicationRecords.Domain.Enumerations;

namespace MedicationRecords.Domain.Models
{
    public class Medication
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public ActiveIngredients ActiveIngredients { get; set; }
        public PharmaceuticalForms PharmaceuticalForms { get; set; }
        public TherapeuticClass TherapeuticClass { get; set; }
        public ATCCode ATCCode { get; set; }
        public Classification Classification { get; set; }
        public Unit Unit { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CompetentAuthorityStatus CompetentAuthorityStatus { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InternalStatus InternalStatus { get; set; }

    }
}
