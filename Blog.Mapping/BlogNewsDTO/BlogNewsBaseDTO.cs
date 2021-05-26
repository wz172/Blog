using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Mapping.BlogNewsDTO
{
   public class BlogNewsBaseDTO
    {
        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string Title { get; set; }

        [SugarColumn(ColumnDataType = "text")]
        public string Content { get; set; }

     

        public int TypeId { get; set; }

       

        public int BrowseCount { get; set; }

        public int LikeCount { get; set; }

        public int AuthorId { get; set; }

      //  [SugarColumn(IsIgnore = true)]
       // public Author Author { get; set; }
    }
}
