using KingPim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class SubCategoryCharacteristicGroupViewModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public IEnumerable<CharacteristicGroup> CharacteristicGroups { get; set; }
        public IEnumerable<int> SelectedCharacteristicGroups { get; set; }
    }
}