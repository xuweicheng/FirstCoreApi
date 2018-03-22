using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FirstCoreApi.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        //[HttpGet("api/cities")]
        [HttpGet()]
        public IActionResult getCities()
        {
            return new JsonResult(new List<object> {
                new { id = 1, name ="Dalian" },
                new { id = 2, name ="Vancouver" },
            });
        }

    }
}