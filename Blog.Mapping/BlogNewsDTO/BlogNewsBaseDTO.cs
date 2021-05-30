using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    如果想对传入的参数数据模型验证，可以使用 System.ComponentModel.DataAnnotations 进行字段标记
    也可以自己写标识方法，来完成数据模型的数据校验 
   */
namespace Blog.Mapping.BlogNewsDTO
{
    public class BlogNewsBaseDTO
    {

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public int TypeId { get; set; }

        public int BrowseCount { get; set; }

        public int LikeCount { get; set; }

        public int AuthorId { get; set; }

        public AuthorBaseDTO Author { get; set; }
    }
}
