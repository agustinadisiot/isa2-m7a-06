using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.Models.In
{
    public class ReviewIntentModel
    {
        public string Text { get; set; }
        public int Stars { get; set; }
        public Guid ReservationId { get; set; }

        public Review ToEntity() 
        {
            return new Review()
            {
                Text = Text,
                Stars = Stars
            };
        }
    }
}
