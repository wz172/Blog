using Blog.Mapping.BlogNewsDTO;
using System;
using System.Collections.Generic;

namespace Blog.Mapping
{
    public class AuthorGetDTO:AuthorBaseDTO
    {
        public int ID { get; set; }
        public IEnumerable<BlogNewsBaseDTO> blogNews { get; set; }
    }
}
