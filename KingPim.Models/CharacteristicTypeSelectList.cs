using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KingPim.Models
{
    public class CharacteristicTypeSelectList
    {
        public int Id { get; set; }
        public int CharacteristicTypeId { get; set; }
        public virtual CharacteristicType CharacteristicType { get; set; }
        [Required(ErrorMessage = "Du måste ange ett värde på attributlista-attributet")]
        public string Value { get; set; }
    }
}