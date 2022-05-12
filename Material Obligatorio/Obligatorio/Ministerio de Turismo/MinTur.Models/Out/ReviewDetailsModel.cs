using MinTur.Domain.BusinessEntities;
using System;

namespace MinTur.Models.Out
{
    public class ReviewDetailsModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int ResortId { get; set; }

        public ReviewDetailsModel(Review review)
        {
            Id = review.Id;
            Text = review.Text;
            Stars = review.Stars;
            Name = review.Name;
            Surname = review.Surname;
            ResortId = review.ResortId;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var reviewDetailsModel = obj as ReviewDetailsModel;
            return Id == reviewDetailsModel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

    }
}
