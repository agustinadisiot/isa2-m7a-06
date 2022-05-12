using Microsoft.EntityFrameworkCore;
using MinTur.DataAccessInterface.Repositories;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using System.Linq;
using MinTur.Exceptions;

namespace MinTur.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected DbContext Context { get; set; }

        public CategoryRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public List<Category> GetAllCategories()
        {
            return Context.Set<Category>().ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            Category retrievedCategory = Context.Set<Category>().
                Where(c => c.Id == categoryId).FirstOrDefault();

            if (retrievedCategory == null)
                throw new ResourceNotFoundException("Could not find specified category");

            return retrievedCategory;
        }
    }
}