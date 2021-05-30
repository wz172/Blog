using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogNewsDTO
{
    public class BlogNewsCreateDTO : BlogNewsBaseDTO
    {
        public DateTime Time { get; set; }
    }
}
