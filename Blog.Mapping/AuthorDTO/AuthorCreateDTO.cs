using Blog.Mapping.BlogNewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping
{
    public class AuthorCreateDTO:AuthorBaseDTO
    {
        public IEnumerable<BlogNewsBaseDTO> blogNews { get; set; }
        public string UserPassword { get; set; }
    }
}
