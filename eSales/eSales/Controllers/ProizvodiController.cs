using eSales.Model;
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

        [HttpGet]
        public IEnumerable<Proizvodi> Get()
        {
            return proizvodiService.Get();
        }
    }
}
