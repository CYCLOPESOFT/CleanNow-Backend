using CleanNow.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Domain.Entities
{
    public class Ranking:AuditableBaseEntity
    {
        public int Rank { get; set; }
        public string UserId { get; set; }
        public string UserSuperId { get; set; }
    }
}
