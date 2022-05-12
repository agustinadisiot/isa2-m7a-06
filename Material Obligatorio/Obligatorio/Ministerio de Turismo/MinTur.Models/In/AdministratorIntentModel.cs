using MinTur.Domain.BusinessEntities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MinTur.Models.In
{
    public class AdministratorIntentModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Administrator ToEntity()
        {
            Administrator administrator = new Administrator()
            {
                Email = Email,
                Password = Password
            };

            return administrator;
        }
    }
}
