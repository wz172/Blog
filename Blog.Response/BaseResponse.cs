using Blog.IResponse;
using BlogModel;
using SqlSugar;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Response
{
    public class BaseResponse<T> : SimpleClient<T>, IBaseResponse<T> where T : class, new()
    {
        public BaseResponse(ISqlSugarClient context=null) : base(context)
        {
            //注入基类中的 Context 在此步骤之前请确定已经注册服务
            base.Context = DbScoped.Sugar;
            //创建数据库
            //base.Context.DbMaintenance.CreateDatabase();
            //创建所有的表
           // base.Context.CodeFirst.InitTables(typeof(BlogNews), typeof(Author), typeof(BlogTypeInfo));
        }
        public async Task<bool> AddAsync(T entity)
        {
            return await base.InsertAsync(entity);
        }

        public async Task DeletAsync(T entity)
        {
            await base.DeleteAsync(entity);
        }

        public async Task<bool> EditAsync(T entity)
        {
            return await base.UpdateAsync(entity);
        }

        public virtual async Task<T> FindAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<T>> QueryAsync()
        {
            return await base.GetListAsync();
        }

        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expression)
        {
            return await base.GetListAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> QueryPageListAsync(Expression<Func<T, bool>> expression, PageModel pageModel)
        {
            return await base.GetPageListAsync(expression, pageModel);
        }
    }
}
