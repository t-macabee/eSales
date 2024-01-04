using AutoMapper;
using eSales.Model;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eSales.Services.Services
{
    public class BaseService<T, TDb, TSearch> : IService<T, TSearch> where T : class where TDb : class where TSearch : BaseSearchObject
    {
        protected EProdajaContext context { get; set; }
        protected IMapper mapper { get; set; }

        public BaseService(EProdajaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public virtual async Task<PagedResult<T>> Get(TSearch? search = null)
        {
            var query = context.Set<TDb>().AsQueryable();

            PagedResult<T> result = new PagedResult<T>();

            query = AddFilter(query, search);

            result.Count = await query.CountAsync();

            if (search?.Page.HasValue == true && search?.PageSize.HasValue == true)
            {
                query = query.Take(search.PageSize.Value).Skip(search.Page.Value * search.PageSize.Value);
            }

            var list = await query.ToListAsync();

            result.Result = mapper.Map<List<T>>(list);

            return result;
        }

        public virtual async Task<T> GetById(int id)
        {
            var entity = await context.Set<TDb>().FindAsync(id);

            return mapper.Map<T>(entity);
        }

        public virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch? search = null) 
        {
            return query;
        }
    }
}
