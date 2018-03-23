using System.Linq;
using FirstCoreApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstCoreApi.Controllers
{
    [Route("api/cities")]
    public class PoiController : Controller
    {
        [HttpGet("{cityId}/poi")]
        public IActionResult GetPois(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.Pois);
        }

        [HttpGet("{cityId}/poi/{poiId}")]
        public IActionResult getPoi(int cityId, int poiId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == poiId);

            if(poi == null)
            {
                return NotFound();
            }

            return Ok(poi);
        }
    }
}
