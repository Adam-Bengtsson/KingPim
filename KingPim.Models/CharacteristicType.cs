using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KingPim.Models
{
    public class CharacteristicType
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste ange ett namn på attributlista")]
        public string Name { get; set; }
    }
}