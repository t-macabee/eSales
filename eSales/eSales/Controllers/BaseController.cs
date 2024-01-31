using eSales.Model;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class BaseController<T, TSearch> : ControllerBase where T : class where TSearch : class
    {
        protected ILogger<BaseController<T, TSearch>> logger;
        protected IService<T, TSearch> service;

        public BaseController(ILogger<BaseController<T, TSearch>> logger, IService<T, TSearch> service)
        {
            this.logger = logger;
            this.service = service;
        }
        
        [HttpGet()]
        public async Task<PagedResult<T>> Get([FromQuery]TSearch? search = null)
        {
            return await service.Get(search);
        }

        [HttpGet("{id}")]
        public async Task<T> GetById(int id)
        {
            return await service.GetById(id);
        }
    }
}
