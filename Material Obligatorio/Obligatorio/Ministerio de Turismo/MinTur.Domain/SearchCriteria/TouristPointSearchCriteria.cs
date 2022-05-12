using MinTur.Domain.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Domain.SearchCriteria
{
    public class TouristPointSearchCriteria : ISearchCriteria<TouristPoint>
    {
        public List<Category> Categories { get; set; }
        public int? RegionId { get; set; }

        public TouristPointSearchCriteria()
        {
            Categories = new List<Category>();
        }

        public bool MatchesCriteria(TouristPoint touristPoint)
        {
            return MatchesCategories(touristPoint) && MatchesRegionId(touristPoint);
        }

        private bool MatchesRegionId(TouristPoint touristPoint)
        {
            bool matchesRegionId = true;

            if (RegionId != null)
                matchesRegionId = touristPoint.RegionId == RegionId;

            return matchesRegionId;
        }

        private bool MatchesCategories(TouristPoint touristPoint)
        {
            bool matchesCategories = true;

            if (Categories == null || Categories.Count == 0)
                matchesCategories = true;
            else
            {
                foreach (Category category in Categories)
                    matchesCategories = matchesCategories && touristPoint.TouristPointCategory.Any(tc => tc.Category.Equals(category));
            }

            return matchesCategories;
        }
    }
}
