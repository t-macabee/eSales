using eSales.Model.Requests.Korisnici;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisniciController : ControllerBase
    {
        private ILogger<KorisniciController> logger;
        private IKorisniciService korisniciService;

        public KorisniciController(ILogger<KorisniciController> logger, IKorisniciService korisniciService)
        {
            this.logger = logger;
            this.korisniciService = korisniciService;
        }

        [HttpGet()]
        public IEnumerable<Model.Korisnici> Get()
        {
            return korisniciService.Get();
        }

        [HttpPost]
        public Model.Korisnici Insert(KorisniciInsertRequest request)
        {
            return korisniciService.Insert(request);
        }

        [HttpPut("{id}")]
        public Model.Korisnici Update(int id, KorisniciUpdateRequest request)
        {
            return korisniciService.Update(id, request);
        }
    }
}
