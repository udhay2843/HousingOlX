using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{[Table("Photos")]
      public class Photo:BaseEntity
    {
        [Required]
         public string? publicId { get; set; }
         
      
         [Required]
         public string? imageUrl { get; set; }
         
         public bool isPrimary { get; set; }
        public int PropertyId { get; set; }
      public Property? Property { get; set; }
    }
}
