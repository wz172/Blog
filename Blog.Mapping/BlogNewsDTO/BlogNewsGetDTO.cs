using Blog.Mapping.BlogTypeInfoDTO;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogNewsDTO
{
   public class BlogNewsGetDTO : BlogNewsBaseDTO
    {
        [SugarColumn(IsIgnore = true)]
        public BlogTypeInfoGetDTO TypeInfo { get; set; }
        public string Time { get; set; }
        public int ID { get; set; }
    }
}
