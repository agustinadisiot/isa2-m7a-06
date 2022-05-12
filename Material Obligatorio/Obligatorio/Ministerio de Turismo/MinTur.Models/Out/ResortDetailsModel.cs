using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Out
{
    public class ResortDetailsModel
    {
        public int Id { get; set; }
        public int TouristPointId { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public List<ImageBasicInfoModel> Images { get; set; }
        public int PricePerNight { get; set; }
        public bool Available { get; set; }
        public List<ReviewDetailsModel> Reviews { get; set; }
        public double Punctuation { get; set; }

        public ResortDetailsModel(Resort resort) 
        {
            Id = resort.Id;
            TouristPointId = resort.TouristPointId;
            Name = resort.Name;
            Id = resort.Id;
            Stars = resort.Stars;
            Address = resort.Address;
            PhoneNumber = resort.PhoneNumber;
            Description = resort.Description;
            Images = resort.Images.Select(i => new ImageBasicInfoModel(i)).ToList();
            Reviews = resort.Reviews.Select(r => new ReviewDetailsModel(r)).ToList();
            PricePerNight = resort.PricePerNight;
            Available = resort.Available;
            Punctuation = resort.Punctuation;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var resortDetailsModel = obj as ResortDetailsModel;
            return Id == resortDetailsModel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
