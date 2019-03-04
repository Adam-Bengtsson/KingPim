using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KingPim.Models;
using KingPim.Repositories;
using KingPim.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using KingPim.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using KingPim.Web.Helpers;

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private ProductHelper _productHelper;
        private IProductRepository productRepo;

        public ProductController(IProductRepository p, ProductHelper ph)
        {
            _productHelper = ph;
            productRepo = p;
        }

        public IActionResult ListProducts()
        {
            var l = new ListProductsViewModel
            {
                Products = productRepo.Products.OrderBy(x => x.Name),
                Categories = productRepo.Categories.OrderBy(x => x.Name)
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult CreateProductStep1()
        {
            if (productRepo.Categories.Count() == 0 || productRepo.SubCategories.Count() == 0)
            {
                TempData["Info"] = "Det finns antingen inga kategorier eller underkategorier";
                return RedirectToAction(nameof(ListProducts));
            }
            return View(productRepo.Categories.OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult CreateProductStep2(int categoryId)
        {
            if (productRepo.SubCategories.Where(x => x.CategoryId.Equals(categoryId)).Count() == 0)
            {
                TempData["Info"] = $"Det saknas underkategorier i kategorin \"{productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)).Name}\"";
                return RedirectToAction(nameof(ListProducts));
            }
            var c = new ProductViewModel
            {
                Product = new Product(),
                SubCategories = productRepo.SubCategories.Where(x => x.CategoryId.Equals(categoryId)).ToSelectList(),
                SelectedCategory = productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)).Name
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult CreateProductStep2(ProductViewModel p)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveProduct(p.Product);
                TempData["Success"] = $"Succé! Produkten \"{p.Product.Name}\" har lagts till";
                return RedirectToAction(nameof(ListProducts));
            }
            else
            {
                return RedirectToAction(nameof(ListProducts));
            }
        }

        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            var p = new ProductViewModel
            {
                Product = productRepo.Products.FirstOrDefault(x => x.Id.Equals(productId)),
                SubCategories = productRepo.SubCategories.OrderBy(x => x.Name).ToSelectListWithCategoryName()
            };
            return View(p);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel p)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveProduct(p.Product);
                TempData["Success"] = $"Succé! Produkten \"{p.Product.Name}\" har ändrats";
                return RedirectToAction(nameof(ListProducts));
            }
            else
            {
                return RedirectToAction(nameof(ListProducts));
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var successMessage = productRepo.DeleteProduct(productId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListProducts));
        }

        [Authorize(Roles = "Administrator, Publisher")]
        [HttpPost]
        public IActionResult PublishProduct(int productId, bool changePublishedValueTo)
        {
            var successMessage = productRepo.PublishProduct(productId, changePublishedValueTo);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListProducts));
        }

        [HttpGet]
        public IActionResult ListProductsWithCategoryFilter(int categoryId)
        {
            if (productRepo.Products.Where(x => x.SubCategory.Category.Id.Equals(categoryId)).Count() == 0)
            {
                TempData["Info"] = $"Det saknas produkter i kategorin \"{productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)).Name}\"";
                return RedirectToAction(nameof(ListProducts));
            }
            var l = new ListProductsViewModel
            {
                Products = productRepo.Products.Where(x => x.SubCategory.CategoryId.Equals(categoryId)),
                SubCategories = productRepo.SubCategories.Where(x => x.CategoryId.Equals(categoryId)),
                SelectedCategory = productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)).Name
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult ListProductsWithSubCategoryFilter(int subCategoryId)
        {
            if (productRepo.Products.Where(x => x.SubCategory.Id.Equals(subCategoryId)).Count() == 0)
            {
                TempData["Info"] = $"Det saknas produkter i underkategorin \"{productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)).Name}\"";
                return RedirectToAction(nameof(ListProducts));
            }
            var l = new ListProductsViewModel
            {
                Products = productRepo.Products.Where(x => x.SubCategoryId.Equals(subCategoryId)),
                SelectedCategory = productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)).Category.Name,
                SelectedSubCategory = productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)).Name
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult EditProductCharacteristicValues(int productId)
        {
            return View(_productHelper.EditProductCharacteristicValues(productId));
        }

        [HttpPost]
        public IActionResult SaveProductCharacteristicValues(int productId, ProductCharacteristicValueViewModel p)
        {
            if (ModelState.IsValid && p.ProductCharacteristicValues != null)
            {
                // Raden nedan tar bort alla "ProductCharacteristicValues" som innehåller rader med ett visst ProductId
                productRepo.DeleteProductCharacteristicValues(productId);
                foreach (var productCharacteristicValue in p.ProductCharacteristicValues)
                {
                    productRepo.SaveProductCharacteristicValue(productCharacteristicValue);
                }
                TempData["Success"] = "Succé! Attributen har sparats";
                return RedirectToAction(nameof(ListProducts));
            }
            else
            {
                return RedirectToAction(nameof(ListProducts));
            }
        }
    }
}