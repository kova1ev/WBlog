using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBlog.Application.Core.Dto
{
    public class PostIndexDto : BaseResponseDto
    {
        public Guid Id { get; set; }
        public string? Slug { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? Title { get; set; }
        public string? Descriprion { get; set; }
    }
}
