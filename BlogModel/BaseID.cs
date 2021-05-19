using System;
using SqlSugar;

namespace BlogModel
{
    public class BaseID
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int ID  { get; set; }
    }
}
