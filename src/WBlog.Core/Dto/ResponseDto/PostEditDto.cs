using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WBlog.Core.Dto.ResponseDto
{
    public class PostEditDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Descriprion { get; set; } = string.Empty;
        [Required]
        public string Contetnt { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
