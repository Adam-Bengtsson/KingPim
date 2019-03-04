using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    [XmlRoot("SubCategories")]
    public class ApiSubCategoryWrapperViewModel
    {
        [XmlElement("SubCategory")]
        public ApiSubCategoryViewModel SubCategory { get; set; }
    }
}