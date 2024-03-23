using CleanNow.Core.Domain.Common;


namespace CleanNow.Core.Domain.Entities
{
    public class Opinion:AuditableBaseEntity
    {
        public int AssistantId { get; set; }
        public Assistant assistant { get; set; }
        public string? Description { get; set; }
        public int Start { get; set; }
        public string ValuerName { get; set; }
    }
}
