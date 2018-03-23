using System.ComponentModel.DataAnnotations;

namespace FirstCoreApi.Controllers
{
    public class PointOfInterestCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}