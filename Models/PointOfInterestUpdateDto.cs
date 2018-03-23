using System.ComponentModel.DataAnnotations;

namespace FirstCoreApi.Models
{
    public class PointOfInterestUpdateDto
    {
        [Required(ErrorMessage = "Please provide name value")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "Max length is 200")]
        public string Description { get; set; }
    }
}