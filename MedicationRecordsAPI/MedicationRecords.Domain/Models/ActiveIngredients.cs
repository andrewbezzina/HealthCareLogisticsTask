using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Models
{
    public class ActiveIngredients
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string ActiveIngredientList { get; set; }
    }
}
