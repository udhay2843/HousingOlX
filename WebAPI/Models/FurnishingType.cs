using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    
         public class FurnishingType:BaseEntity
    { 
         [Required]
         public string? Name { get; set; }
    }
    }
