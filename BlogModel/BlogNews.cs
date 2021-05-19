using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace BlogModel
{
    public class BlogNews : BaseID
    {

        //public string AuthorName { get; set; }

        [SugarColumn(ColumnDataType ="nvarchar(32)")]
        public string Title { get; set; }

        [SugarColumn(ColumnDataType ="text")]
        public string Content { get; set; }
        
        public DateTime Time { get; set; }

        public int TypeId { get; set; }

        [SugarColumn(IsIgnore =true)]
        public BlogTypeInfo  TypeInfo { get; set; }

        public int BrowseCount { get; set; }

        public int LikeCount { get; set; }

        public int AuthorId { get; set; }
        [SugarColumn(IsIgnore =true)]
        public Author Author { get; set; }
    }
}
