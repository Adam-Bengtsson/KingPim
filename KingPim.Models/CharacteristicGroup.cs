using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KingPim.Models
{
    public class CharacteristicGroup
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste ange ett attributsgruppsnamn")]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int Version { get; set; }
    }
}