using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogTypeInfoDTO
{
   public class BlogTypeInfoCreateDTO :BlogTypeInfoBaseDTO
    {
        public BlogNewsDTO.BlogNewsCreateDTO BlogNews { get; set; }
    }
}
