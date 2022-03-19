using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBlog.Domain.Entity
{
    public class Post : BaseEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Descriprion { get; set; }
        public string Contetnt { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
