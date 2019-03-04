using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace KingPim.Models
{
    [DataContract]
    public class ProductCharacteristicValue
    {
        [Key]
        public int ProductId { get; set; }
        [Key]
        public int CharacteristicId { get; set; }
        [DataMember]
        public string CharacteristicName { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Du måste ange ett värde på attributet")]
        public string Value { get; set; }
    }
}