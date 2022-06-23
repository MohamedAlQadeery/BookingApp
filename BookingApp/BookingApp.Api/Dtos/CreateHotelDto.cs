using System.ComponentModel.DataAnnotations;

namespace BookingApp.Api.Dtos
{
    public class CreateHotelDto
    {
        //[Required(AllowEmptyStrings =false,ErrorMessage ="الاسم فارغ")]
        [Required]
        public string? Name { get; set; }
        [Range(0,5)]
        public int Stars { get; set; }
        public string? Address { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public string? Description { get; set; }
    }
}
