using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping
{
    public class AuthorCreateDTO:AuthorBaseDTO
    {
        public string UserPassword { get; set; }
    }
}
