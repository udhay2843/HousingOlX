using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos
{
    public class CityDto
    {
         public int Id { get; set; }
           [Required(ErrorMessage ="Name is required")]
        [StringLength(8,MinimumLength=3)]
        public string? Name { get; set; }
         [Required(ErrorMessage ="enter country name")]
        //[RegularExpression("^[A-Z]+$",ErrorMessage ="Only allow Caps letter")]
         [StringLength(8,MinimumLength=3)]
        public string? Country { get; set; }
    }
}