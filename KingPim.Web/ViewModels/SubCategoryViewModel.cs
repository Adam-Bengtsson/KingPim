using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class SubCategoryViewModel
    {
        public SubCategory SubCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}