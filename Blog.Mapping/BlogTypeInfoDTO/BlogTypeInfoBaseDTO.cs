using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogTypeInfoDTO
{
  public  class BlogTypeInfoBaseDTO
    {
        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string TypeName { get; set; }

        //类型不生成到数据库字段
       //[SugarColumn(IsIgnore = true)]
       // public BlogNews BlogNews { get; set; }
    }
}
