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
                    Description = "Beautiful City"
                },
                new CityDto {
                    Id = 2,
                    Name ="Vancouver",
                    Description = "Another Beautiful City"
                },
                new CityDto {
                    Id = 3,
                    Name ="Beijing",
                    Description = "Very Big City"
                },
            };
        }
    }
}
