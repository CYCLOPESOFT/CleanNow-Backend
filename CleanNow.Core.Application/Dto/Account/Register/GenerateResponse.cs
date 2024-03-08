using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Account.Register
{
    public class GenerateResponse
    {
        public int StatusCode { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }
        public string Error { get; set; }
        public bool HasError { get; set; }
    }
}
