using MinTur.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class ChargingPoint
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(30)]
        public  string Address { get; set; }
        
        [Required]
        [MaxLength(60)]
        public string Description { get; set; }
        
        [Required]
        public int RegionId { get; set; }

    }
}
