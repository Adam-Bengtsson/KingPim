using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace KingPim.Models
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Du måste ange ett produktnamn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Du måste välja en subkategori")]
        public int SubCategoryId { get; set; }
        [DataMember]
        public virtual SubCategory SubCategory { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int Version { get; set; }
        public bool Published { get; set; }
        [DataMember]
        public virtual List<ProductCharacteristicValue> ProductCharacteristicValues { get; set; }
    }
}