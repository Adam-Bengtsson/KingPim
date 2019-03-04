using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    [XmlRoot("Categories")]
    public class ApiCategoryWrapperViewModel
    {
        [XmlElement("Category")]
        public ApiCategoryViewModel Category { get; set; }
    }
}