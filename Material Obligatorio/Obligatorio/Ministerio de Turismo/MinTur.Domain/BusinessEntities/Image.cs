using MinTur.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinTur.Domain.BusinessEntities
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Data { get; set; }

        public Image() { }

        public virtual void ValidOrFail() 
        {
            ValidateData();
        }
        
        public void ValidateData() 
        {
            if (Data == null)
                throw new InvalidRequestDataException("Must provide image data");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var image = obj as Image;
            return Id == image.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
