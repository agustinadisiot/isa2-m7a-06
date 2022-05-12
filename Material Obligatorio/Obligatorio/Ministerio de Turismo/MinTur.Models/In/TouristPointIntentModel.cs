using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;

namespace MinTur.Models.In
{
    public class TouristPointIntentModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int RegionId { get; set; }
        public List<int> CategoriesId { get; set; }

        public TouristPointIntentModel()
        {
            CategoriesId = new List<int>();
        }

        public TouristPoint ToEntity()
        {
            TouristPoint touristPoint = new TouristPoint()
            {
                Name = Name,
                Description = Description,
                Image = new Image() { Data = Image },
                RegionId = RegionId,
            };
            if(CategoriesId != null)
                CategoriesId.ForEach(c => touristPoint.AddCategory(new Category() { Id = c }));

            return touristPoint;
        }

    }
}
