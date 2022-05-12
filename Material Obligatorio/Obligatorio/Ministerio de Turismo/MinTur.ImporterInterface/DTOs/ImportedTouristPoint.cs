using System.Collections.Generic;
using System.Linq;

namespace MinTur.ImporterInterface.DTOs
{
    public class ImportedTouristPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int RegionId { get; set; }
        public List<int> CategoriesId { get; set; }

        public ImportedTouristPoint()
        {
            CategoriesId = new List<int>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var touristPoint = obj as ImportedTouristPoint;
            return Name == touristPoint.Name && Id == touristPoint.Id && Description == touristPoint.Description
                && Image == touristPoint.Image && RegionId == touristPoint.RegionId
                && CategoriesId.SequenceEqual(touristPoint.CategoriesId);
        }
    }
}
