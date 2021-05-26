using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace BlogModel
{
  public class Author:BaseID
    {
        [SugarColumn(ColumnDataType ="nvarchar(32)")]
        public string   Name { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(32)")]
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        [SugarColumn(IsIgnore =true)]
        public IEnumerable<BlogNews> blogNews { get; set; }
    }
}
