using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KingPim.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KingPim.Infrastructure;
using KingPim.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace KingPim.Web.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private IProductRepository productRepo;

        public ExportController(IProductRepository p)
        {
            productRepo = p;
        }

        public IActionResult Index()
        {
            var e = new ExportViewModel
            {
                Categories = productRepo.Categories.Where(x => x.Published.Equals(true)),
                SubCategories = productRepo.SubCategories.Where(x => x.Published.Equals(true))
            };
            return View(e);
        }
    }
}