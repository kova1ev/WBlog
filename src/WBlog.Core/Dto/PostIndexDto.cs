using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBlog.Core.Dto
{
    public class PostIndexDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string? Title { get; set; }
        public string? Descriprion { get; set; }
        public string? ImagePath { get; set; }
    }
}
