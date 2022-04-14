using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBlog.Application.Core.Dto
{
    public class PostDetailsDto : BaseResponseDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? Slug { get; set; }
        public string? Title { get; set; }
        public string? Descriprion { get; set; }
        public string? Contetnt { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
