using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanEasy.Infrastructured.Identity.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string LastActivity { get; set; }
        public bool State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? SessionAt { get; set; }

    }
}
