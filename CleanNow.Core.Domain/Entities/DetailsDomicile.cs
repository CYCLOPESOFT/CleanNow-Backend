using CleanNow.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Domain.Entities
{
    public class DetailsDomicile:AuditableBaseEntity
    {
        public string Addresses { get; set; }
        public string? Apt { get; set; }
        public string? TypeClean { get; set; }
        public string Time { get; set; }
        public string ImageDomicile { get; set; }
    }
}
