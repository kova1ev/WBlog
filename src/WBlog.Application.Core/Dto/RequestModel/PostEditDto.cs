﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WBlog.Application.Core.Dto
{
    public class PostEditDto : BaseRequestDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Required]
        public string Descriprion { get; set; } = string.Empty;
        [Required]
        public string Contetnt { get; set; } = string.Empty;

        [Required]
        public IEnumerable<string> Tags { get; set; } = Enumerable.Empty<string>();
    }
}
