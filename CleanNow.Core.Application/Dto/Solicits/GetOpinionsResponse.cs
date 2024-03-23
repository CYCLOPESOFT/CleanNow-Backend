using CleanNow.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Solicits
{
    public class GetSolicitsResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime SelectedDate { get; set; }
        public int HiringId { get; set; }
    }
}
