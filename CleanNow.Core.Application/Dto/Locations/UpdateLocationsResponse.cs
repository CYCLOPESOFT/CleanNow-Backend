using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Locations
{
    public class UpdateLocationsResponse
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string Apt { get; set; }
        public string Doorbell { get; set; }
        public string City { get; set; }

    }
}
