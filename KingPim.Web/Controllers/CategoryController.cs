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

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private IProductRepository productRepo;

        public CategoryController(IProductRepository p)
        {
            productRepo = p;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCategory(c);
                TempData["Success"] = $"Succé! Kategori \"{c.Name}\" har lagts till";
                return RedirectToAction(nameof(ListCategories));
            }
            else
            {
                return RedirectToAction(nameof(ListCategories));
            }
        }

        [HttpGet]
        public IActionResult ListCategories()
        {
            if (productRepo.Categories.Count() == 0)
            {
                TempData["Info"] = "Det finns inga kategorier";
                return RedirectToAction(nameof(ListCategories));
            }
            return View(productRepo.Categories.OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult EditCategory(int categoryId)
        {
            return View(productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)));
        }

        [HttpPost]
        public IActionResult EditCategory(Category c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCategory(c);
                TempData["Success"] = $"Succé! Kategorin har ändrats till \"{c.Name}\"";
                return RedirectToAction(nameof(ListCategories));
            }
            else
            {
                return RedirectToAction(nameof(ListCategories));
            }
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var successMessage = productRepo.DeleteCategory(categoryId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCategories));
        }

        [Authorize(Roles = "Administrator, Publisher")]
        [HttpPost]
        public IActionResult PublishCategory(int categoryId, bool changePublishedValueTo)
        {
            var successMessage = productRepo.PublishCategory(categoryId, changePublishedValueTo);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCategories));
        }

        [HttpGet]
        public IActionResult CreateSubCategory()
        {
            if (productRepo.Categories.Count() == 0)
            {
                TempData["Info"] = "Det finns inga kategorier";
                return RedirectToAction(nameof(ListSubCategories));
            }
            var c = new SubCategoryViewModel
            {
                SubCategory = new SubCategory(),
                Categories = productRepo.Categories.OrderBy(x => x.Name).ToSelectList()
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult CreateSubCategory(SubCategoryViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveSubCategory(c.SubCategory);
                TempData["Success"] = $"Succé! Underkategori \"{c.SubCategory.Name}\" har lagts till";
                return RedirectToAction(nameof(ListSubCategories));
            }
            else
            {
                return RedirectToAction(nameof(ListSubCategories));
            }
        }

        [HttpGet]
        public IActionResult ListSubCategories()
        {
            if (productRepo.Categories.Count() == 0 || productRepo.SubCategories.Count() == 0)
            {
                TempData["Info"] = "Det finns antingen inga kategorier eller underkategorier";
                return RedirectToAction(nameof(ListSubCategories));
            }
            var l = new ListSubCategoriesViewModel
            {
                Categories = productRepo.Categories.OrderBy(x => x.Name),
                SubCategories = productRepo.SubCategories.OrderBy(x => x.Name)
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult ListSubCategoriesWithCategoryFilter(int categoryId)
        {
            if (productRepo.SubCategories.Where(x => x.CategoryId.Equals(categoryId)).Count() == 0)
            {
                TempData["Info"] = $"Det saknas underkategorier i kategorin \"{productRepo.Categories.FirstOrDefault(x => x.Id.Equals(categoryId)).Name}\"";
                return RedirectToAction(nameof(ListSubCategories));
            }
            return View(productRepo.SubCategories.Where(x => x.CategoryId.Equals(categoryId)).OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult EditSubCategory(int subCategoryId)
        {
            var s = new SubCategoryViewModel
            {
                SubCategory = productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)),
                Categories = productRepo.Categories.OrderBy(x => x.Name).ToSelectList()
            };
            return View(s);
        }

        [HttpPost]
        public IActionResult EditSubCategory(SubCategoryViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveSubCategory(c.SubCategory);
                TempData["Success"] = "Succé! Underkategorin har ändrats";
                return RedirectToAction(nameof(ListSubCategories));
            }
            else
            {
                return RedirectToAction(nameof(ListSubCategories));
            }
        }

        [HttpGet]
        public IActionResult EditSubCategoryCharacteristicGroups(int subCategoryId)
        {
            var c = new SubCategoryCharacteristicGroupViewModel
            {
                SubCategoryId = subCategoryId,
                SubCategoryName = productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)).Name,
                CharacteristicGroups = productRepo.CharacteristicGroups.OrderBy(x => x.Name),
                SelectedCharacteristicGroups = productRepo.SubCategoryCharacteristicGroups.Where(x => x.SubCategoryId.Equals(subCategoryId)).Select(x => x.CharacteristicGroupId)
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult EditSubCategoryCharacteristicGroups(int subCategoryId, string characteristicGroupsToAdd)
        {
            // Raden nedan tar bort alla "SubCategoryCharacteristicGroups" som innehåller rader med ett visst SubCategoryId
            productRepo.DeleteSubCategoryCharacteristicGroups(subCategoryId);
            // Raderna nedan lägger till alla nya nya "SubCategoryCharacteristicGroups" som valts
            if (characteristicGroupsToAdd != null)
            {
                int[] vehiclesIdToAddArray = Array.ConvertAll(characteristicGroupsToAdd.Split(','), int.Parse);
                foreach (var item in vehiclesIdToAddArray)
                {
                    productRepo.AddSubCategoryCharacteristicGroup(subCategoryId, item);
                }
                TempData["Success"] = $"Succé! Attributgrupperna har ändrats på underkategori \"{productRepo.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId)).Name}\"";
            }
            return RedirectToAction(nameof(ListSubCategories));
        }

        [HttpPost]
        public IActionResult DeleteSubCategory(int subCategoryId)
        {
            var successMessage = productRepo.DeleteSubCategory(subCategoryId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListSubCategories));
        }

        [Authorize(Roles = "Administrator, Publisher")]
        [HttpPost]
        public IActionResult PublishSubCategory(int subCategoryId, bool changePublishedValueTo)
        {
            var successMessage = productRepo.PublishSubCategory(subCategoryId, changePublishedValueTo);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListSubCategories));
        }
    }
}