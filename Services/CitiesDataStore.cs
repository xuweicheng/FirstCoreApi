using FirstCoreApi.Models;
using System.Collections.Generic;

namespace FirstCoreApi.Services
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto> {
                new CityDto {
                    Id = 1,
                    Name ="Dalian",
                    Description = "Beautiful City",
                    Pois = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id = 1,
                            Name = "Stars and Sea park",
                            Description = "Beautiful park along seashore"
                        },
                        new PointOfInterestDto
                        {
                            Id = 2,
                            Name = "Labor's park",
                            Description = "A park for labors, not a good name"
                        }
                    }
                },
                new CityDto {
                    Id = 2,
                    Name ="Vancouver",
                    Description = "Another Beautiful City",
                    Pois = new List<PointOfInterestDto> {
                        new PointOfInterestDto
                        {
                            Id = 3,
                            Name = "Stanley park",
                            Description = "Beautiful park as a small island"
                        },
                        new PointOfInterestDto
                        {
                            Id = 4,
                            Name = "QE park",
                            Description = "at central Vancouver"
                        }
                    }
                },
                new CityDto {
                    Id = 3,
                    Name ="Beijing",
                    Description = "Very Big City",
                    Pois = new List<PointOfInterestDto>{
                        new PointOfInterestDto
                        {
                            Id = 5,
                            Name = "Forbidden city",
                            Description = "it's forbidden"
                        }
                    }
                }
            };
        }
    }
}
