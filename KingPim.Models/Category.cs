using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace KingPim.Models
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public int Id { get; set; }
        [Required(ErrorMessage = "Du måste ange ett kategorinamn")]
        [DataMember]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int Version { get; set; }
        public bool Published { get; set; }
    }
}