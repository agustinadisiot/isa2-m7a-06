using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using Microsoft.AspNetCore.Mvc;
using MinTur.Models.CustomBinders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.In
{
    [ModelBinder(BinderType = typeof(TouristPointCategoriesSearchModelBinder))]
    public class TouristPointSearchModel
    {
        public List<int> CategoriesId { get; set; }
        public int? RegionId { get; set; }

        public TouristPointSearchModel() 
        {
            CategoriesId = new List<int>();
        }

        public TouristPointSearchCriteria ToEntity() 
        {
            TouristPointSearchCriteria entity = new TouristPointSearchCriteria();

            foreach(int categoryId in CategoriesId) 
            {
                entity.Categories.Add(new Category() { Id = categoryId });
            }
            entity.RegionId = RegionId;

            return entity;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var touristPointSearchCriteriaModel = obj as TouristPointSearchModel;

            return CategoriesId.All(touristPointSearchCriteriaModel.CategoriesId.Contains) && RegionId == touristPointSearchCriteriaModel.RegionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CategoriesId);
        }
    }
}
