using System.Linq;
using FirstCoreApi.Models;
using FirstCoreApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstCoreApi.Controllers
{
    [Route("api/cities")]
    public class PoiController : Controller
    {
        private ILogger<PoiController> _logger;
        private IMailService _mailService;

        public PoiController(
            ILogger<PoiController> logger,
            IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("{cityId}/poi")]
        public IActionResult GetPois(int cityId)
        {
            try
            {
                //throw new System.Exception("My Test Exception");
                var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city == null)
                {
                    _logger.LogInformation($"city {cityId} is not found.");
                    return NotFound();
                }

                return Ok(city.Pois);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("exception thrown", ex);
                //throw exception will generate 500 error, we handle it with a user friendly message. 
                //not to expose implemtation details
                //throw; 
                return StatusCode(500, "There is an error, consumer friendly message.");
            }
        }

        [HttpGet("{cityId}/poi/{id}", Name = "GetPoi")]
        public IActionResult getPoi(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            return Ok(poi);
        }

        [HttpPost("{cityId}/poi")]
        public IActionResult CreatePoi(int cityId,
            [FromBody] PointOfInterestCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }

            if (createDto.Name.Equals(createDto.Description))
            {
                ModelState.AddModelError("Description", "Name can not be the same with Description");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
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

        [HttpPut("{cityId}/poi/{id}")]
        public IActionResult UpdatePoi(int cityId, int id, [FromBody] PointOfInterestUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest();
            }

            if (updateDto.Name.Equals(updateDto.Description))
            {
                ModelState.AddModelError("Description", "Name can not be the same with Description");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            poi.Name = updateDto.Name;
            poi.Description = updateDto.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/poi/{id}")]
        public IActionResult PartialUpdatePoi(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            //The ModelState does NOT work for JsonPatchDocument
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState); 
            //}

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            var updateDto = new PointOfInterestUpdateDto()
            {
                Name = poi.Name,
                Description = poi.Description
            };


            //Throws ArgumentNullException if not valid JsonPatchDocument
            patchDocument.ApplyTo(updateDto, ModelState); //this ApplyTo Only works for JsonPatchDocuemnt<T>
                                                          //ModelState now contains error of <T> type

            if (updateDto.Name == updateDto.Description)
            {
                ModelState.AddModelError("Description", "Name can not be the same with Description");
            }

            TryValidateModel(updateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            poi.Name = updateDto.Name;
            poi.Description = updateDto.Description;

            _mailService.Send("poi updated", "poi updated");


            return NoContent();
        }

        [HttpDelete("{cityId}/poi/{id}")]
        public IActionResult DeletePoi(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var poi = city.Pois.FirstOrDefault(p => p.Id == id);

            if (poi == null)
            {
                return NotFound();
            }

            city.Pois.Remove(poi);

            return NoContent();
        }
    }
}
