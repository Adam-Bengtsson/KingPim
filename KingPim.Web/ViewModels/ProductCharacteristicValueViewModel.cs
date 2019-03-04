using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class ProductCharacteristicValueViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<CharacteristicGroup> CharacteristicGroups { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public IEnumerable<ProductCharacteristicValue> ExistingProductCharacteristicValues { get; set; }
        public List<ProductCharacteristicValue> ProductCharacteristicValues { get; set; }
        public List<CharacteristicTypeSelectList> CharacteristicTypeSelectLists { get; set; }
    }
}