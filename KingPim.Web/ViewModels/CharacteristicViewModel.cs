using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class CharacteristicViewModel
    {
        public Characteristic Characteristic { get; set; }
        public List<SelectListItem> CharacteristicGroups { get; set; }
        public List<SelectListItem> CharacteristicTypes { get; set; }
    }
}