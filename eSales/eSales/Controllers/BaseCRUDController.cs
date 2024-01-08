using eSales.Model;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseCRUDController<T, TSearch, TInsert, TUpdate> : BaseController<T, TSearch> where TSearch : class where T : class
    {
        protected new ILogger<BaseController<T, TSearch>> logger;
        protected new ICRUDService<T, TSearch, TInsert, TUpdate> service;

        public BaseCRUDController(ILogger<BaseController<T, TSearch>> logger, ICRUDService<T, TSearch, TInsert, TUpdate> service) : base(logger, service) 
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<T> Insert([FromBody]TInsert insert)
        {
            return await service.Insert(insert);
        }

        [HttpPut("{id}")]
        public virtual async Task<T> Update(int id, [FromBody]TUpdate update)
        {
            return await service.Update(id, update);
        }
    }
}
