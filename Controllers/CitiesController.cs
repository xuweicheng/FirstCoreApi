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
        public IActionResult GetCities() {
            //var result = new JsonResult(CitiesDataStore.Current.Cities)
            //{
            //    StatusCode = 200
            //};
            //return result;

            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if(city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}