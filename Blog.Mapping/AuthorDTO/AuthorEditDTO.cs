using Blog.Mapping.BlogNewsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.AuthorDTO
{
    public class AuthorEditDTO : AuthorBaseDTO
    {
        public int ID { get; set; }
        public string UserPassword { get; set; }

        public IEnumerable<BlogNewsBaseDTO> blogNews { get; set; }
    }
}
