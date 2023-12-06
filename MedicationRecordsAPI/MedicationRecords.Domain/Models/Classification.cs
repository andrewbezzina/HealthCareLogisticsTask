using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class Classification
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(5)]
        public string ClassificationName { get; set; }
    }
}
