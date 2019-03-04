using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace KingPim.Models
{
    public class Characteristic
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste ange ett attributnamn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Du måste ange en attributgrupp")]
        public int CharacteristicGroupId { get; set; }
        public virtual CharacteristicGroup CharacteristicGroup { get; set; }
        [Required(ErrorMessage = "Du måste ange typ av attribut")]
        public int CharacteristicTypeId { get; set; }
        public virtual CharacteristicType CharacteristicType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int Version { get; set; }
    }
}