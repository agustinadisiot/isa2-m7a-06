using System;
using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.Out
{
    public class RegionBasicInfoModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public RegionBasicInfoModel(Region region) 
        {
            Id = region.Id;
            Name = region.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var region = obj as RegionBasicInfoModel;
            return Id == region.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
