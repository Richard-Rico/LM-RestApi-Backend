using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMRestApi2023.Models;

namespace LMRestApi2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        // private readonly northwindContext db = new northwindContext();

        // Dependency Injection tyyli
        private readonly Context db;

        public DocumentationController(Context dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        [Route("{keycode}")]
        public ActionResult GetAll(string keycode)
        {
            var paths = db.Documentations.Where(d => d.Keycode == keycode);
            return Ok(paths);

        }

    }
}
