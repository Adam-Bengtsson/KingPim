using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class ListCharacteristicsViewModel
    {
        public IEnumerable<CharacteristicGroup> CharacteristicGroups { get; set; }
        public IEnumerable<Characteristic> Characteristics { get; set; }
    }
}