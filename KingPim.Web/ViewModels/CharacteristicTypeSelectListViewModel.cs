using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class CharacteristicTypeSelectListViewModel
    {
        public CharacteristicTypeSelectList CharacteristicTypeSelectList { get; set; }
        public List<SelectListItem> CharacteristicTypes { get; set; }
    }
}
