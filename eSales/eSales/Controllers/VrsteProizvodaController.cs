using eSales.Model;
using eSales.Model.SearchObjects;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VrsteProizvodaController : BaseController<Model.VrsteProizvoda, BaseSearchObject>
    {
        public VrsteProizvodaController(ILogger<BaseController<VrsteProizvoda, BaseSearchObject>> logger, IService<Model.VrsteProizvoda, BaseSearchObject> service) 
            : base(logger, service)
        {
        }
    }
}
