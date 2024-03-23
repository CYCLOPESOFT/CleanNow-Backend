using CleanNow.Core.Domain.Common;

namespace CleanNow.Core.Domain.Entities
{
    public class Like:AuditableBaseEntity
    {
        public string UserId { get; set; }
        public int AssistantId { get; set; }
        public bool isLike { get; set; }
        public Assistant assistant { get; set; }    
    }
}
