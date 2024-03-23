using CleanNow.Core.Domain.Common;

namespace CleanNow.Core.Domain.Entities
{
    public class Location:AuditableBaseEntity
    {
        public string Address { get; set; }
        public string Street { get; set; }
        public string Apt { get;set; }
        public string Doorbell { get; set; }
        public string City { get; set; }
        public ICollection<Hiring> hirings { get; set; }
    }
}
