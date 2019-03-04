using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    public class ApiCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlElement("Subcategories")]
        public ApiCategorySubCategoryWrapper SubCategories { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}