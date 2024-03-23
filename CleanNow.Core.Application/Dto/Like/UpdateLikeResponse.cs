using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Dto.Like
{
    public class UpdateLikeResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AssistantId { get; set; }
        public bool isLike { get; set; }
        public string NameAssistant { get; set; }

    }
}
