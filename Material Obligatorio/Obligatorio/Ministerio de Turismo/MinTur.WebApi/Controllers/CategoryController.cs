using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Category> retrievedCategories = _categoryManager.GetAllCategories();
            List<CategoryBasicInfoModel> categoriesModels = retrievedCategories.Select(category => new CategoryBasicInfoModel(category)).ToList();
            return Ok(categoriesModels);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSpecificCategory(int id)
        {
            Category retrievedCategory = _categoryManager.GetCategoryById(id);
            CategoryBasicInfoModel categoryModel = new CategoryBasicInfoModel(retrievedCategory);
            return Ok(categoryModel);
        }
    }
}