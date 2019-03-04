using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    [XmlRoot("Categories")]
    public class ApiCategoriesWrapperViewModel
    {
        [XmlElement("Category")]
        public List<ApiCategoryViewModel> Categories { get; set; }
    }
}