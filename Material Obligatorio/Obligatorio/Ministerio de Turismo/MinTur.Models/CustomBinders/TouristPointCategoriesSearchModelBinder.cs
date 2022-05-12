using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinTur.Models.In;
using System;
using System.Threading.Tasks;

namespace MinTur.Models.CustomBinders
{
    public class TouristPointCategoriesSearchModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var categoriesId = bindingContext.ValueProvider.GetValue("categoriesId");
            var regionId = bindingContext.ValueProvider.GetValue("regionId");
            var result = new TouristPointSearchModel();

            if(categoriesId != ValueProviderResult.None)
            {
                var individualCategoriesId = categoriesId.FirstValue.Split('-');

                foreach(var categoryId in individualCategoriesId)
                {
                    if (!int.TryParse(categoryId, out int categoryIdParsed))
                    {
                        bindingContext.ModelState.TryAddModelError(
                            "CategoriesId", "CategoriesId must be integers separed by '-'.");
                        return Task.CompletedTask;
                    }
                    else
                        result.CategoriesId.Add(categoryIdParsed);
                }
            }
            if(regionId != ValueProviderResult.None)
            {
                if (!int.TryParse(regionId.FirstValue, out int regionIdParsed))
                {
                    bindingContext.ModelState.TryAddModelError(
                        "RegionId", "RegionId must be an integer");
                    return Task.CompletedTask;
                }
                else
                    result.RegionId = regionIdParsed;
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
