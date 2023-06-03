using Microsoft.AspNetCore.Mvc;
using StarDeckAPI.Data;

namespace StarDeckAPI.Controllers
{
    [ApiController]
    [Route("Parametros")]
    public class ParametroController : Controller
    {
        private APIDbContext apiDBContext;
        public ParametroController(APIDbContext apiDBContext)
        {
            this.apiDBContext = apiDBContext;
        }
        [HttpGet]
        [Route("getParametros")]
        public IActionResult getParametros()
        {
            return Ok(this.apiDBContext.Parametros.ToList());
        }
    }
}
