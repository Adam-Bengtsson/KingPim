using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KingPim.Models
{
    public class SubCategoryCharacteristicGroup
    {
        [Key]
        public int SubCategoryId { get; set; }
        [Key]
        public int CharacteristicGroupId { get; set; }
    }
}