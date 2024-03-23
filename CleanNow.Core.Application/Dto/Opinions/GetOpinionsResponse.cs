using CleanNow.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Opinions
{
    public class GetOpinionsResponse
    {
        public int Id { get; set; }
        public int AssistantId { get; set; }
        public string AssistantName { get; set; }
        public string? Description { get; set; }
        public int Start { get; set; }
        public string ValuerName { get; set; }
    }
}
