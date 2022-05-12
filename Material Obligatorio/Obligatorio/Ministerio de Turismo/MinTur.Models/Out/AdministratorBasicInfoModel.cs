using System;
using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.Out
{
    public class AdministratorBasicInfoModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public AdministratorBasicInfoModel(Administrator administrator)
        {
            Id = administrator.Id;
            Email = administrator.Email;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var administrator = obj as AdministratorBasicInfoModel;
            return Id == administrator.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
