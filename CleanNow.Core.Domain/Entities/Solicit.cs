using CleanNow.Core.Domain.Common;

namespace CleanNow.Core.Domain.Entities
{
    public class Solicit:AuditableBaseEntity
    {
        public bool Status { get; set; }
        public DateTime SelectedDate { get; set; }
        public int HiringId { get; set; }
        public Hiring hiring { get; set; }
    }
}
