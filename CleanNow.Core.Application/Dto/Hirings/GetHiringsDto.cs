
using CleanNow.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Hirings
{
    public class GetHiringsDto
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; }
        public int AssistentId { get; set; }
        public string NameAssistant { get; set; }
        public string UserId { get; set; }
        public string PayType { get; set; }
        public string Total { get; set; }
        public string Meter { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
