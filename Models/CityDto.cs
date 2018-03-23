using System.Collections.Generic;

namespace FirstCoreApi.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int NumberOfPois { get
            {
                return Pois.Count;
            }
        }

        public ICollection<PointOfInterestDto> Pois { get; set; } = new List<PointOfInterestDto>();
    }
}
