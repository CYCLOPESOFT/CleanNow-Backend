using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Account.Register
{
    public class RegisterRequest
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Apellido { get; set; }
        public string Email { get; set; }
        public string? Image { get; set; }
    }
}
