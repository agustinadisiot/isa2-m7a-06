using System;
using MinTur.Domain.BusinessEntities;

namespace MinTur.Models.Out
{
    public class CategoryBasicInfoModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public CategoryBasicInfoModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var categoryModel = obj as CategoryBasicInfoModel;
            return Id == categoryModel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
