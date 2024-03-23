using CleanNow.Core.Domain.Common;

namespace CleanNow.Core.Domain.Entities
{
    public class Hiring:AuditableBaseEntity
    {
        public int LocationId { get; set; }
        public Location location { get; set; }
        public int AssistentId { get; set; }
        public Assistant assistant { get; set; }
        public string UserId { get; set; }
        public string PayType { get; set; }
        public string Total {  get; set; }
        public string Meter { get; set; }
        public ICollection<Solicit> solicit { get; set; }
    }
}
