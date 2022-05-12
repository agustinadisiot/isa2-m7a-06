using MinTur.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MinTur.Domain.BusinessEntities
{
    public class Administrator
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }

        public Administrator() { }

        public virtual void ValidOrFail() 
        {
            ValidateEmail();
            ValidatePassword();
        }

        public virtual void Update(Administrator newAdministrator) 
        {
            Email = newAdministrator.Email;
            Password = newAdministrator.Password;
        }

        private void ValidatePassword()
        {
            if (Password == null)
                throw new InvalidRequestDataException("Must provide a password");
        }

        private void ValidateEmail()
        {
            Regex mailRegex = new Regex(@"^[a-zA-Z0-9_]+\@[a-zA-Z0-9_]+\.[a-zA-Z]+\.*[a-zA-Z]*\.*[a-zA-Z]*$");

            if (Email == null || !mailRegex.IsMatch(Email))
                throw new InvalidRequestDataException("Invalid email");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var administrator = obj as Administrator;
            return Id == administrator.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
