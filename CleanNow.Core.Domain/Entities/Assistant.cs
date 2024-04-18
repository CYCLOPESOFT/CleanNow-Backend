using CleanNow.Core.Domain.Common;


namespace CleanNow.Core.Domain.Entities
{
    public class Assistant:AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }
        public int Experience { get; set; }
        public bool IsVerify { get; set; }
        public double Price { get; set; }
        public int Availability { get; set; }
        public string Image { get; set; }
        public ICollection<Hiring> hirings { get; set; }
        public ICollection<Opinion> opinions { get; set; }

        public ICollection<Like> likes { get; set; }

    }
}
