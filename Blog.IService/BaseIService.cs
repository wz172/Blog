using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.IService
{
    public interface BaseIService<T> where T :class ,new ()
    {
        Task<bool> AddAsync(T entity);
        Task DeletAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<T> FindAsync(int id);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync();

        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expression);
#pragma warning disable CS0246 // 未能找到类型或命名空间名“PageModel”(是否缺少 using 指令或程序集引用?)
        public Task<IEnumerable<T>> QueryPageListAsync(Expression<Func<T, bool>> expression, PageModel pageModel);
#pragma warning restore CS0246 // 未能找到类型或命名空间名“PageModel”(是否缺少 using 指令或程序集引用?)
    }
}
