using MedicationRecords.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationRecords.Infrastructure.DataAccess
{
    public class MedicationContext : DbContext
    {
        public MedicationContext(DbContextOptions options) : base(options) { }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<ActiveIngredients> ActiveIngredients { get; set; }
        public DbSet<PharmaceuticalForms> PharmaceuticalForms { get; set; }
        public DbSet<TherapeuticClass> TherapeuticClasses { get; set; }
        public DbSet<ATCCode> ATCCodes { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Unit> Units { get; set; }
    }
}
