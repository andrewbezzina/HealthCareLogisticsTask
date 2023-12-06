using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class ATCCode
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
    }
}
