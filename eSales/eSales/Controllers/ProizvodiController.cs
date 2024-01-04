using eSales.Services.Database;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProizvodiController : ControllerBase
    {
        private ILogger<ProizvodiController> logger;
        private IProizvodiService proizvodiService;

        public ProizvodiController(ILogger<ProizvodiController> logger, IProizvodiService proizvodiService)
        {
            this.logger = logger;
            this.proizvodiService = proizvodiService;
        }

        [HttpGet()]
        public async Task<IEnumerable<Model.Proizvodi>> Get()
        {
            return await proizvodiService.Get();
        }
    }
}
