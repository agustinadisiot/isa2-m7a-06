using MinTur.Domain.BusinessEntities;

namespace MinTur.Domain.SearchCriteria
{
    public class ResortSearchCriteria : ISearchCriteria<Resort>
    {
        public int? TouristPointId { get; set; }
        public bool? Available { get; set; }

        public ResortSearchCriteria() { }

        public bool MatchesCriteria(Resort resort)
        {
            return MatchesTouristPoint(resort) && MatchesAvailability(resort);
        }

        private bool MatchesAvailability(Resort resort)
        {
            bool matchesAvailability = true;

            if(Available != null)
                matchesAvailability = resort.Available == Available;

            return matchesAvailability;
        }

        private bool MatchesTouristPoint(Resort resort)
        {
            bool matchesTouristPoint = true;

            if(TouristPointId != null)
                matchesTouristPoint =  resort.TouristPointId == TouristPointId;

            return matchesTouristPoint;
        }
    }
}
