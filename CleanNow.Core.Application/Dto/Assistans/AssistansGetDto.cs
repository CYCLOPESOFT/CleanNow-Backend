using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Assistans
{
    public class AssistansGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string AboutMe { get; set; }
        public int Experience { get; set; }
        public bool IsVerify { get; set; }
        public double Price { get; set; }
        public int Availability { get; set; }
        public int CountLikes { get; set; }
    }
}
