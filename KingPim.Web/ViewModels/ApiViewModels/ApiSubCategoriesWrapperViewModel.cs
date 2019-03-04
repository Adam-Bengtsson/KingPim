using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    [XmlRoot("SubCategories")]
    public class ApiSubCategoriesWrapperViewModel
    {
        [XmlElement("SubCategory")]
        public List<ApiSubCategoryViewModel> SubCategories { get; set; }
    }
}