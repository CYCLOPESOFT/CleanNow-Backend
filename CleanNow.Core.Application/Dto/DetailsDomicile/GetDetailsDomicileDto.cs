using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.DetailsDomicile
{
    public class GetDetailsDomicileDto
    {
        public int Id { get; set; }
        public string Addresses { get; set; }
        public string? Apt { get; set; }
        public string? TypeClean { get; set; }
        public string ImageDomicile { get; set; }
        public string UserId { get; set; }
    }
}
