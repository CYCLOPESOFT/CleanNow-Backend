using CleanNow.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Domain.Entities
{
    public class Category:AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }

}
