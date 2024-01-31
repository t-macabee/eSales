using eSales.Model;
using eSales.Model.SearchObjects;
using eSales.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSales.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class JediniceMjereController : BaseController<Model.JediniceMjere, JediniceMjereSearchObject>
    {               
        public JediniceMjereController(ILogger<BaseController<JediniceMjere, JediniceMjereSearchObject>> logger, IJediniceMjereService service) : base(logger, service) 
        { 
        }        
    }
}
