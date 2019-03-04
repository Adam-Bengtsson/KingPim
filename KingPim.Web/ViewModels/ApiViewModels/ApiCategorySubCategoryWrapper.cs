using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    public class ApiCategorySubCategoryWrapper
    {
        [XmlElement("Subcategory")]
        public List<string> Names { get; set; }

        public ApiCategorySubCategoryWrapper()
        {
            this.Names = new List<string>();
        }
    }
}