using CleanNow.Core.Domain.Common;

namespace CleanNow.Core.Domain.Entities
{
    public class DetailsDomicile:AuditableBaseEntity
    {
        public string Addresses { get; set; }
        public string? Apt { get; set; }
        public string? TypeClean { get; set; }
        public string Time { get; set; }
        public string ImageDomicile { get; set; }
        public string UserId { get; set; }
    }
}
