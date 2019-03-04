using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    //[XmlRoot("type")]
    //[XmlType("newName")]
    //[DataContract]
    //[JsonObject("Test")]
    public class ApiProductViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime? UpdatedDate { get; set; }
        [JsonIgnore]
        public ApiProductCharacteristicWrapperValuesViewModel Characteristics { get; set; }
        [XmlIgnoreAttribute]
        public List<ApiProductCharacteristicValuesViewModel> Characteristic { get; set; }
}
}