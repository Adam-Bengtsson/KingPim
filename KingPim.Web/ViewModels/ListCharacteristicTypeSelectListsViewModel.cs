using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class ListCharacteristicTypeSelectListsViewModel
    {
        public IEnumerable<CharacteristicTypeSelectList> CharacteristicTypeSelectLists { get; set; }
        public IEnumerable<CharacteristicType> CharacteristicsTypes { get; set; }
    }
}