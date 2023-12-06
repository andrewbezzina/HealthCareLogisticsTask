using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class PharmaceuticalForms
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string PharmaceuticalFormsList { get; set; }
    }
}
