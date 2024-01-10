 using eSales.Model.Requests.Proizvodi;
using eSales.Model.SearchObjects;
using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProizvodiController : BaseCRUDController<Model.Proizvodi, ProizvodiSearchObject, ProizvodiInsertRequest, ProizvodiUpdateRequest>
    {
        public ProizvodiController(ILogger<BaseController<Model.Proizvodi, ProizvodiSearchObject>> logger, IProizvodiService service) : base(logger, service)
        {
        }

        [HttpPut("{id}/activate")]
        public async Task<Model.Proizvodi> Activate(int id)
        {
            return await (service as IProizvodiService).Activate(id);
        }

        [HttpPut("{id}/hide")]
        public async Task<Model.Proizvodi> Hide(int id)
        {
            return await (service as IProizvodiService).Hide(id);
        }

        [HttpGet("{id}/allowedActions")]
        public virtual async Task<List<string>> AllowedActions(int id)
        {
            return await (service as IProizvodiService).AllowedActions(id);
        }
    }
}
