using System.Collections.Generic;
using MinTur.Domain.BusinessEntities;

namespace MinTur.DataAccessInterface.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategoryById(int id);
    }
}