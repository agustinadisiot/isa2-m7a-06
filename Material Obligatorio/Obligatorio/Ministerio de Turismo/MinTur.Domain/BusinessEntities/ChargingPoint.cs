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

        [Required]
        public Region Region { get; set; }

        public virtual void ValidOrFail()
        {
            ValidateName();
           ValidateDescription();
           ValidateAddress();
        }

        public void ValidateId()
        {
            if ( Id.ToString().Length != 4)
                throw new InvalidRequestDataException("Invalid charging point id");
        }

        private void ValidateName()
        {
            Regex nameRegex = new Regex(@"^[a-zA-ZñÑáéíóúü0-9 ]+$");

            if (Name == null || !nameRegex.IsMatch(Name) || Name.Length > 20)
                throw new InvalidRequestDataException("Invalid charging point name");
        }
        
        private void ValidateDescription() 
        {
            Regex descriptionRegex = new Regex(@"^[a-zA-ZñÑáéíóúü0-9 ]+$");
            if (Description == null || Description.Length > 60 || !descriptionRegex.IsMatch(Description))
                throw new InvalidRequestDataException("Invalid charging point description");
        }
        
        private void ValidateAddress() 
        {
            Regex addressRegex = new Regex(@"^[a-zA-ZñÑáéíóúü0-9 ]+$");
            if (Address == null || Address.Length > 30 || !addressRegex.IsMatch(Address))
                throw new InvalidRequestDataException("Invalid charging point address");
        }
    }
}
