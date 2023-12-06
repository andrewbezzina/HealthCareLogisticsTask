using System.ComponentModel.DataAnnotations;

namespace MedicationRecords.Domain.Enumerations
{
    public enum CompetentAuthorityStatus
    {
        Unknown = 0,
        Authorised = 1,
        Withdrawn = 2,
        Suspended = 3
    }
}
