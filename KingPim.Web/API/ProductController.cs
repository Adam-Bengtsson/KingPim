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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/product
        [HttpGet]
        public IActionResult GetProducts()
        {
            var listApiProductViewModel = new List<ApiProductViewModel>();
            foreach (var product in _context.Products.
                Where(x => x.Published.Equals(true)).
                Where(x => x.SubCategory.Published.Equals(true)).
                Where(x => x.SubCategory.Category.Published.Equals(true)))
            {
                var apiProductViewModel = new ApiProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.SubCategory.Category.Name,
                    SubCategoryName = product.SubCategory.Name,
                    UpdatedDate = product.UpdatedDate,
                    Characteristics = CreateApiProductCharacteristicWrapperValuesViewModel(product.Id),
                    Characteristic = CreateListApiProductCharacteristicValuesViewModel(product.Id)
                };
                listApiProductViewModel.Add(apiProductViewModel);
            }
            return Ok(new ApiProductsWrapperViewModel { Products = listApiProductViewModel });
        }

        // GET: api/product/1
        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var product = _context.Products.
                Where(x => x.Published.Equals(true)).
                Where(x => x.SubCategory.Published.Equals(true)).
                Where(x => x.SubCategory.Category.Published.Equals(true)).
                FirstOrDefault(x => x.Id == id);

            var apiProductViewModel = new ApiProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.SubCategory.Category.Name,
                SubCategoryName = product.SubCategory.Name,
                UpdatedDate = product.UpdatedDate,
                Characteristics = CreateApiProductCharacteristicWrapperValuesViewModel(id),
                Characteristic = CreateListApiProductCharacteristicValuesViewModel(id)
            };

            return Ok(new ApiProductWrapperViewModel { Product = apiProductViewModel });
        }

        // GET: api/product/subcategory/1
        [HttpGet("subcategory/{id}")]
        public IActionResult GetProductsFromSubCategory([FromRoute] int id)
        {

            var listApiProductViewModel = new List<ApiProductViewModel>();

            foreach (var product in _context.Products.
                Where(x => x.Published.Equals(true)).
                Where(x => x.SubCategory.Published.Equals(true)).
                Where(x => x.SubCategory.Category.Published.Equals(true)).
                Where(x => x.SubCategory.Equals(id)))
            {
                var apiProductViewModel = new ApiProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.SubCategory.Category.Name,
                    SubCategoryName = product.SubCategory.Name,
                    UpdatedDate = product.UpdatedDate,
                    Characteristics = CreateApiProductCharacteristicWrapperValuesViewModel(product.Id),
                    Characteristic = CreateListApiProductCharacteristicValuesViewModel(product.Id)
                };

                listApiProductViewModel.Add(apiProductViewModel);
            }
            return Ok(new ApiProductsWrapperViewModel { Products = listApiProductViewModel });
        }

        // GET: api/product/category/1
        [HttpGet("category/{id}")]
        public IActionResult GetProductsFromCategory([FromRoute] int id)
        {
            var listApiProductViewModel = new List<ApiProductViewModel>();
            foreach (var product in _context.Products.
                Where(x => x.Published.Equals(true)).
                Where(x => x.SubCategory.Published.Equals(true)).
                Where(x => x.SubCategory.Category.Published.Equals(true)).
                Where(x => x.SubCategory.CategoryId.Equals(id)))
            {
                var apiProductViewModel = new ApiProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    CategoryName = product.SubCategory.Category.Name,
                    SubCategoryName = product.SubCategory.Name,
                    UpdatedDate = product.UpdatedDate,
                    Characteristics = CreateApiProductCharacteristicWrapperValuesViewModel(product.Id),
                    Characteristic = CreateListApiProductCharacteristicValuesViewModel(product.Id)
                };
                listApiProductViewModel.Add(apiProductViewModel);
            }
            return Ok(new ApiProductsWrapperViewModel { Products = listApiProductViewModel });
        }

        public ApiProductCharacteristicWrapperValuesViewModel CreateApiProductCharacteristicWrapperValuesViewModel(int Id)
        {
            var apiProductCharacteristicWrapperValuesViewModel = new ApiProductCharacteristicWrapperValuesViewModel();
            foreach (var productCharacteristicValue in _context.ProductCharacteristicValues.Where(x => x.ProductId.Equals(Id)))
            {
                var x = new ApiProductCharacteristicValuesViewModel()
                {
                    Name = _context.Characteristics.Find(productCharacteristicValue.CharacteristicId).Name,
                    Value = productCharacteristicValue.Value
                };
                apiProductCharacteristicWrapperValuesViewModel.Characteristics.Add(x);
            }
            return (apiProductCharacteristicWrapperValuesViewModel);
        }

        public List<ApiProductCharacteristicValuesViewModel> CreateListApiProductCharacteristicValuesViewModel(int Id)
        {
            var listApiProductCharacteristicValuesViewModel = new List<ApiProductCharacteristicValuesViewModel>();
            foreach (var productCharacteristicValue in _context.ProductCharacteristicValues.Where(x => x.ProductId.Equals(Id)))
            {
                var x = new ApiProductCharacteristicValuesViewModel()
                {
                    Name = _context.Characteristics.Find(productCharacteristicValue.CharacteristicId).Name,
                    Value = productCharacteristicValue.Value
                };
                listApiProductCharacteristicValuesViewModel.Add(x);
            }
            return (listApiProductCharacteristicValuesViewModel);
        }
    }
}