using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.IResponse
{
    /*
        增删改查 共有的提取到父类中
     */
    public interface IBaseResponse<T> where T: class,new ()
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
        public Task<IEnumerable<T>> QueryPageListAsync(Expression<Func<T, bool>> expression, PageModel pageModel);
    }
}
