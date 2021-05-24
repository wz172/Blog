using Blog.IResponse;
using Blog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Service
{
    public class BlogBaseService<T> : IBaseService<T> where T : class, new()
    {
        protected IBaseResponse<T> baseResponse;
        public BlogBaseService(IBaseResponse<T> response)
        {
            baseResponse = response;
        }
        public async Task<bool> AddAsync(T entity)
        {
            return await baseResponse.AddAsync(entity);
        }

        public async Task DeletAsync(T entity)
        {
             await baseResponse.DeletAsync(entity);
        }

        public async Task<bool> EditAsync(T entity)
        {
            return await baseResponse.EditAsync(entity);
        }

        public async Task<T> FindAsync(int id)
        {
            return await baseResponse.FindAsync(id); 
        }

        public async Task<IEnumerable<T>> QueryAsync()
        {
            return await baseResponse.QueryAsync();
        }

        public async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> expression)
        {
            return await baseResponse.QueryAsync(expression);
        }

        public async Task<IEnumerable<T>> QueryPageListAsync(Expression<Func<T, bool>> expression, PageModel pageModel)
        {
            return await baseResponse.QueryPageListAsync(expression, pageModel);
        }
    }
}
