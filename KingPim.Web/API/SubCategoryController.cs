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
    public class SubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/subcategory
        [HttpGet]
        public IActionResult GetSubCategories()
        {
            var listApiSubCategoryViewModel = new List<ApiSubCategoryViewModel>();
            foreach (var subCategory in _context.SubCategories.
                Where(x => x.Published.Equals(true)).
                Where(x => x.Category.Published.Equals(true)))
            {
                var apiSubCategoryViewModel = new ApiSubCategoryViewModel
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    Category = subCategory.Category.Name,
                    UpdatedDate = subCategory.UpdatedDate
                };
                listApiSubCategoryViewModel.Add(apiSubCategoryViewModel);
            }
            return Ok(new ApiSubCategoriesWrapperViewModel { SubCategories = listApiSubCategoryViewModel });
        }

        // GET: api/subcategory/5
        [HttpGet("{id}")]
        public IActionResult GetSubCategory([FromRoute] int id)
        {
            var subCategory =_context.SubCategories.
                Where(x => x.Published.Equals(true)).
                Where(x => x.Category.Published.Equals(true)).
                FirstOrDefault(x => x.Id == id);
            var apiSubCategoryViewModel = new ApiSubCategoryViewModel
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Category = subCategory.Category.Name,
                UpdatedDate = subCategory.UpdatedDate
            };
            return Ok(new ApiSubCategoryWrapperViewModel { SubCategory = apiSubCategoryViewModel });
        }
    }
}