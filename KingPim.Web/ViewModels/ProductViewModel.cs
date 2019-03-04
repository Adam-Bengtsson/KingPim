using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<SelectListItem> SubCategories { get; set; }
        public string SelectedCategory { get; set; }
    }
}