using FirstCoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstCoreApi.Controllers
{
    //[Route("api/cities")]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        //[HttpGet("api/cities")]
        [HttpGet()]
        public IActionResult GetCities() => new JsonResult(CitiesDataStore.Current.Cities);

        [HttpGet("{id}")]
        public IActionResult GetCity(int id) => new JsonResult(
                CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
    }
}