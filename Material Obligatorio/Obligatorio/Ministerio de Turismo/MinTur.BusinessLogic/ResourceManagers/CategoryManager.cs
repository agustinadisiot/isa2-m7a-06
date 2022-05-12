using System.Collections.Generic;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;

namespace MinTur.BusinessLogic.ResourceManagers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IRepositoryFacade _repositoryFacade;

        public CategoryManager(IRepositoryFacade repositoryFacade)
        {
            _repositoryFacade = repositoryFacade;
        }

        public List<Category> GetAllCategories()
        {
            return _repositoryFacade.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _repositoryFacade.GetCategoryById(categoryId);
        }
    }
}