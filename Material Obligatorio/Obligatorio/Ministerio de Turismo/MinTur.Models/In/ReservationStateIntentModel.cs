using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.In
{
    public class ReservationStateIntentModel
    {
        public string Description { get; set; }
        public string State { get; set; }

        public ReservationStateIntentModel() { }

        public ReservationState ToEntity() 
        {
            return new ReservationState()
            {
                State = State,
                Description = Description
            };
        }

    }
}
