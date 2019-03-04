using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KingPim.Repositories;
using KingPim.Models;
using Microsoft.AspNetCore.Mvc;
using KingPim.Web.ViewModels;
using KingPim.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class CharacteristicController : Controller
    {
        private IProductRepository productRepo;

        public CharacteristicController(IProductRepository p)
        {
            productRepo = p;
        }

        [HttpGet]
        public IActionResult CreateCharacteristicGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCharacteristicGroup(CharacteristicGroup c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicGroup(c);
                TempData["Success"] = $"Succé! Attributsgruppen \"{c.Name}\" har lagts till";
                return RedirectToAction(nameof(ListCharacteristicGroups));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicGroups));
            }

        }

        [HttpGet]
        public IActionResult ListCharacteristicGroups()
        {
            if (productRepo.CharacteristicGroups.Count() == 0)
            {
                TempData["Info"] = "Det finns inga attributgrupper";
                return RedirectToAction(nameof(ListCharacteristicGroups));
            }
            return View(productRepo.CharacteristicGroups.OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult EditCharacteristicGroup(int characteristicGroupId)
        {
            return View(productRepo.CharacteristicGroups.FirstOrDefault(x => x.Id.Equals(characteristicGroupId)));
        }

        [HttpPost]
        public IActionResult EditCharacteristicGroup(CharacteristicGroup c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicGroup(c);
                TempData["Success"] = $"Succé! Attributgruppsnamnet har ändrats till \"{c.Name}\"";
                return RedirectToAction(nameof(ListCharacteristicGroups));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicGroups));
            }
        }

        [HttpPost]
        public IActionResult DeleteCharacteristicGroup(int characteristicGroupId)
        {
            var successMessage = productRepo.DeleteCharacteristicGroup(characteristicGroupId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCharacteristicGroups));
        }

        [HttpGet]
        public IActionResult CreateCharacteristic()
        {
            if (productRepo.CharacteristicGroups.Count() == 0)
            {
                TempData["Info"] = "Det finns inga attributgrupper";
                return RedirectToAction(nameof(ListCharacteristics));
            }
            var c = new CharacteristicViewModel
            {
                Characteristic = new Characteristic(),
                CharacteristicGroups = productRepo.CharacteristicGroups.OrderBy(x => x.Name).ToSelectList(),
                CharacteristicTypes = productRepo.CharacteristicTypes.ToSelectList()
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult CreateCharacteristic(CharacteristicViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristic(c.Characteristic);
                TempData["Success"] = $"Succé! Attributet \"{c.Characteristic.Name}\" har lagts till";
                return RedirectToAction(nameof(ListCharacteristics));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristics));
            }
        }

        [HttpGet]
        public IActionResult ListCharacteristics()
        {
            if (productRepo.CharacteristicGroups.Count() == 0 || productRepo.Characteristics.Count() == 0)
            {
                TempData["Info"] = "Det finns antingen inga attributgrupper eller attribut";
                return RedirectToAction(nameof(ListCharacteristics));
            }
            var l = new ListCharacteristicsViewModel
            {
                CharacteristicGroups = productRepo.CharacteristicGroups.OrderBy(x => x.Name),
                Characteristics = productRepo.Characteristics.OrderBy(x => x.Name)
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult ListCharacteristicsWithCharacteristicGroupFilter(int characteristicGroupId)
        {
            if (productRepo.Characteristics.Where(x => x.CharacteristicGroupId.Equals(characteristicGroupId)).Count() == 0)
            {
                TempData["Info"] = $"Det saknas attribut i attributgruppen \"{productRepo.CharacteristicGroups.FirstOrDefault(x => x.Id.Equals(characteristicGroupId)).Name}\"";
                return RedirectToAction(nameof(ListCharacteristics));
            }
            return View(productRepo.Characteristics.Where(x => x.CharacteristicGroupId.Equals(characteristicGroupId)).OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult EditCharacteristic(int characteristicId)
        {
            var c = new CharacteristicViewModel
            {
                Characteristic = productRepo.Characteristics.FirstOrDefault(x => x.Id.Equals(characteristicId)),
                CharacteristicGroups = productRepo.CharacteristicGroups.OrderBy(x => x.Name).ToSelectList(),
                CharacteristicTypes = productRepo.CharacteristicTypes.OrderBy(x => x.Name).ToSelectList()
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult EditCharacteristic(CharacteristicViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristic(c.Characteristic);
                TempData["Success"] = "Succé! Attributet har ändrats";
                return RedirectToAction(nameof(ListCharacteristics));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristics));
            }
        }

        [HttpPost]
        public IActionResult DeleteCharacteristic(int characteristicId)
        {
            var successMessage = productRepo.DeleteCharacteristic(characteristicId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCharacteristics));
        }

        [HttpGet]
        public IActionResult ListCharacteristicTypes()
        {
            if (productRepo.CharacteristicTypes.Skip(2).Count() == 0)
            {
                TempData["Info"] = "Det finns inga attributlistor";
            }
            return View(productRepo.CharacteristicTypes.Skip(2).OrderBy(x => x.Name));
        }

        [HttpGet]
        public IActionResult CreateCharacteristicType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCharacteristicType(CharacteristicType c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicType(c);
                TempData["Success"] = $"Succé! Attributlistan \"{c.Name}\" har lagts till";
                return RedirectToAction(nameof(ListCharacteristicTypes));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicTypes));
            }
        }

        [HttpGet]
        public IActionResult EditCharacteristicType(int characteristicTypeId)
        {
            return View(productRepo.CharacteristicTypes.FirstOrDefault(x => x.Id.Equals(characteristicTypeId)));
        }

        [HttpPost]
        public IActionResult EditCharacteristicType(CharacteristicType c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicType(c);
                TempData["Success"] = $"Succé! Attributgruppsnamnet har ändrats till \"{c.Name}\"";
                return RedirectToAction(nameof(ListCharacteristicTypes));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicTypes));
            }
        }

        [HttpPost]
        public IActionResult DeleteCharacteristicType(int characteristicTypeId)
        {
            var successMessage = productRepo.DeleteCharacteristicType(characteristicTypeId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCharacteristicTypes));
        }

        [HttpGet]
        public IActionResult ListCharacteristicTypeSelectLists()
        {
            if (productRepo.CharacteristicTypeSelectLists.Count() == 0)
            {
                TempData["Info"] = "Det finns inga attributlista-attribut";
            }
            var l = new ListCharacteristicTypeSelectListsViewModel
            {
                CharacteristicsTypes = productRepo.CharacteristicTypes.Skip(2).OrderBy(x => x.Name),
                CharacteristicTypeSelectLists = productRepo.CharacteristicTypeSelectLists.OrderBy(x => x.Value)
            };
            return View(l);
        }

        [HttpGet]
        public IActionResult ListCharacteristicTypeSelectListsWithCharacteristicTypeFilter(int characteristicTypeId)
        {
            if (productRepo.CharacteristicTypeSelectLists.Where(x => x.CharacteristicTypeId.Equals(characteristicTypeId)).Count() == 0)
            {
                TempData["Info"] = "Det saknas attribut i attributlistan";
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
            return View(productRepo.CharacteristicTypeSelectLists.Where(x => x.CharacteristicTypeId.Equals(characteristicTypeId)).OrderBy(x => x.Value));
        }

        [HttpGet]
        public IActionResult CreateCharacteristicTypeSelectList()
        {
            if (productRepo.CharacteristicTypes.Count() == 0)
            {
                TempData["Info"] = "Det finns inga attributlistor";
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
            var c = new CharacteristicTypeSelectListViewModel
            {
                CharacteristicTypeSelectList = new CharacteristicTypeSelectList(),
                CharacteristicTypes = productRepo.CharacteristicTypes.Skip(2).OrderBy(x => x.Name).ToSelectList()
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult CreateCharacteristicTypeSelectList(CharacteristicTypeSelectListViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicTypeSelectList(c.CharacteristicTypeSelectList);
                TempData["Success"] = $"Succé! Attributlista-attributet \"{c.CharacteristicTypeSelectList.Value}\" har lagts till";
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
        }

        [HttpGet]
        public IActionResult EditCharacteristicTypeSelectList(int characteristicTypeSelectListId)
        {
            var c = new CharacteristicTypeSelectListViewModel
            {
                CharacteristicTypeSelectList = productRepo.CharacteristicTypeSelectLists.FirstOrDefault(x => x.Id.Equals(characteristicTypeSelectListId)),
                CharacteristicTypes = productRepo.CharacteristicTypes.Skip(2).OrderBy(x => x.Name).ToSelectList()
            };
            return View(c);
        }

        [HttpPost]
        public IActionResult EditCharacteristicTypeSelectList(CharacteristicTypeSelectListViewModel c)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveCharacteristicTypeSelectList(c.CharacteristicTypeSelectList);
                TempData["Success"] = $"Succé! Attributlista-attributet har ändrats till \"{c.CharacteristicTypeSelectList.Value}\"";
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
            else
            {
                return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
            }
        }

        [HttpPost]
        public IActionResult DeleteCharacteristicTypeSelectList(int characteristicTypeSelectListId)
        {
            var successMessage = productRepo.DeleteCharacteristicTypeSelectList(characteristicTypeSelectListId);
            TempData["Success"] = successMessage;
            return RedirectToAction(nameof(ListCharacteristicTypeSelectLists));
        }
    }
}