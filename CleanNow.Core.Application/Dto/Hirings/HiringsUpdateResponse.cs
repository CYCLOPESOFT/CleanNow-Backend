using CleanNow.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Hirings
{
    public class HiringsUpdateResponse
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location location { get; set; }
        public int AssistentId { get; set; }
        public Assistant assistant { get; set; }
        public string UserId { get; set; }
        public string PayType { get; set; }
        public string Total { get; set; }
        public string Meter { get; set; }
    }
}
