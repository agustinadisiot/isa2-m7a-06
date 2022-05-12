using System;
using System.Collections.Generic;
using System.Linq;
using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.Out
{
    public class TouristPointBasicInfoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageBasicInfoModel Image { get; set; }
        public RegionBasicInfoModel Region { get; set; }
        public List<CategoryBasicInfoModel> Categories { get; private set; }

        public TouristPointBasicInfoModel(TouristPoint touristPoint)
        {
            Id = touristPoint.Id;
            Name = touristPoint.Name;
            Description = touristPoint.Description;
            Region = new RegionBasicInfoModel(touristPoint.Region);
            Categories = touristPoint.TouristPointCategory.Select(t => t.Category).Select(category => new CategoryBasicInfoModel(category)).ToList();
            if (touristPoint.Image != null)
                Image = new ImageBasicInfoModel(touristPoint.Image);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var touristPointModel = obj as TouristPointBasicInfoModel;
            return Id == touristPointModel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
