using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class TherapeuticClass
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string TherapeuticClassName { get; set; }
    }
}
