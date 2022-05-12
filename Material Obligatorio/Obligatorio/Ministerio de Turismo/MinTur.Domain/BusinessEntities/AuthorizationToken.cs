using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinTur.Domain.BusinessEntities
{
    public class AuthorizationToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        [Required]
        public Administrator Administrator { get; set; }
        [Required]
        public DateTime ValidSince { get; set; }

        public AuthorizationToken()
        {
            ValidSince = DateTime.Now;
        }
    }
}
