using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KingPim.DataAccess;
using KingPim.Models;
using Microsoft.AspNetCore.Authorization;
using KingPim.Web.ViewModels;

namespace KingPim.Web.API
{
    [Authorize]
    [Route("api/{format?}/[controller]")]
    [FormatFilter]
    [Produces("application/json", "application/xml")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var listApiCategoryViewModel = new List<ApiCategoryViewModel>();
            foreach (var category in _context.Categories.
                Where(x => x.Published.Equals(true)))
            {
                var listSubCategoriesInCategory = new ApiCategorySubCategoryWrapper();
                foreach (var subCategory in _context.SubCategories.Where(x => x.CategoryId.Equals(category.Id)))
                {
                    listSubCategoriesInCategory.Names.Add(subCategory.Name);
                };
                var apiCategoryViewModel = new ApiCategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    SubCategories = listSubCategoriesInCategory,
                    UpdatedDate = category.UpdatedDate
                };
                listApiCategoryViewModel.Add(apiCategoryViewModel);
            }
            return Ok(new ApiCategoriesWrapperViewModel { Categories = listApiCategoryViewModel });
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public IActionResult GetCategory([FromRoute] int id)
        {
            var category = _context.Categories.
                Where(x => x.Published.Equals(true)).
                FirstOrDefault(x => x.Id == id);
            var listSubCategoriesInCategory = new ApiCategorySubCategoryWrapper();
            foreach (var subCategory in _context.SubCategories.Where(x => x.CategoryId.Equals(category.Id)))
            {
                listSubCategoriesInCategory.Names.Add(subCategory.Name);
            };

            var apiCategoryViewModel = new ApiCategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                SubCategories = listSubCategoriesInCategory,
                UpdatedDate = category.UpdatedDate
            };
            return Ok(new ApiCategoryWrapperViewModel { Category = apiCategoryViewModel });
        }
    }
}