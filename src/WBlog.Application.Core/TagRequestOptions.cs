using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBlog.Application.Core
{
    public class TagRequestOptions : PageOptions
    {
        public string? Query { get; set; }
    }
}
