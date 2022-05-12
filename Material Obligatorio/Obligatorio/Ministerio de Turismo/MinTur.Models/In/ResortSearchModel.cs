using MinTur.Domain.SearchCriteria;

namespace MinTur.Models.In
{
    public class ResortSearchModel
    {
        public int? TouristPointId { get; set; }
        public bool? Available { get; set; }
        public bool ClientSearch { get; set; }

        public ResortSearchModel() { }

        public ResortSearchCriteria ToEntity()
        {
            return new ResortSearchCriteria()
            {
                TouristPointId = TouristPointId,
                Available = Available
            };
        }
    }
}
