using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string UnitName { get; set; }
    }
}
