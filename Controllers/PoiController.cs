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

        [HttpGet("{cityId}/poi/{id}", Name = "GetPoi")]
        public IActionResult getPoi(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == id);

            if(poi == null)
            {
                return NotFound();
            }

            return Ok(poi);
        }

        [HttpPost("{cityId}/poi")]
        public IActionResult CreatePoi(int cityId,
            [FromBody] PointOfInterestCreateDto createDto)
        {
            if(createDto == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }

            var maxPoiId = CitiesDataStore.Current.Cities.SelectMany(c => c.Pois).Max(p => p.Id);

            var newPoi = new Models.PointOfInterestDto
            {
                Id = ++maxPoiId,
                Name = createDto.Name,
                Description = createDto.Description
            };

            city.Pois.Add(newPoi);

            return CreatedAtRoute("GetPoi", 
                new { cityId = cityId, id = newPoi.Id }, 
                newPoi);
        }
    }
}
