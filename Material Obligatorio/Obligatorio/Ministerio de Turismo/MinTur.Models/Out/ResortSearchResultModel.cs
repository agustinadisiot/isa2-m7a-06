using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Out
{
    public class ResortSearchResultModel
    {
        public int Id { get; set; }
        public int TouristPointId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public List<ImageBasicInfoModel> Images { get; set; }
        public int PricePerNight { get; set; }
        public int TotalPrice { get; set; }
        public double Punctuation { get; set; }

        public ResortSearchResultModel(Resort resort) 
        {
            Id = resort.Id;
            Name = resort.Name;
            Stars = resort.Stars;
            Address = resort.Address;
            Description = resort.Description;
            Images = resort.Images.Select(i => new ImageBasicInfoModel(i)).ToList();
            PricePerNight = resort.PricePerNight;
            TouristPointId = resort.TouristPointId;
            Punctuation = resort.Punctuation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var resort = obj as ResortSearchResultModel;
            return Id == resort.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }

}
