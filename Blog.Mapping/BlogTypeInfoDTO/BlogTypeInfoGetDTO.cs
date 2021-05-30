using Blog.Mapping.BlogNewsDTO;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogTypeInfoDTO
{
    public class BlogTypeInfoGetDTO : BlogTypeInfoBaseDTO
    {
        //类型不生成到数据库字段
        [SugarColumn(IsIgnore = true)]
        public BlogNewsGetDTO BlogNews { get; set; }
        public int ID { get; set; }
    }
}
