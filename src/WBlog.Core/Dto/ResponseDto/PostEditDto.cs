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
        [Required]
        public string? Title { get; set; } 
        [Required]
        public string? Descriprion { get; set; }  
        [Required]
        public string? Contetnt { get; set; } 
        [Required]
        public string? ImagePath { get; set; }
        [Required]
        public IEnumerable<string>? Tags { get; set; } 
    }
}
