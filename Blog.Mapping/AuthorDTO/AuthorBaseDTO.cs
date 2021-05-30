using Blog.Mapping.BlogNewsDTO;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping
{
    public class AuthorBaseDTO
    {
        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string Name { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string UserName { get; set; }
    }
}
