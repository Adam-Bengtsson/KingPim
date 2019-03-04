using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    public class ApiSubCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlElement("Category")]
        public string Category { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}