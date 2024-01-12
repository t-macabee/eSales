using eSales.Model;
using eSales.Model.Requests.Korisnici;
using eSales.Model.SearchObjects;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisniciController : BaseCRUDController<Model.Korisnici, KorisniciSearchObject, KorisniciInsertRequest, KorisniciUpdateRequest>
    {
        public KorisniciController(ILogger<BaseController<Korisnici, KorisniciSearchObject>> logger, IKorisniciService service) : base(logger, service)
        {
        }

        [Authorize(Roles = "Administrator")]
        public override Task<Korisnici> Insert([FromBody] KorisniciInsertRequest insert)
        {
            return base.Insert(insert);
        }
    }
}
