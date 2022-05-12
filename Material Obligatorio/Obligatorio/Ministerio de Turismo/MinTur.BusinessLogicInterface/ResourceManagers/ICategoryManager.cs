using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;

namespace MinTur.BusinessLogicInterface.ResourceManagers
{
    public interface ICategoryManager
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int categoryId);
    }
}