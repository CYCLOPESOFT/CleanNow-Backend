using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Account.Forgot
{
    public class ForgotResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
