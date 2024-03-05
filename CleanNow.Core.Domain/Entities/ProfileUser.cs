using CleanNow.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Domain.Entities
{
    public class ProfileUser:AuditableBaseEntity
    {
        public string HourPay { get; set; }
        public DateTime YearExperience { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
    }
}
