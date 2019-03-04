using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    [XmlRoot("Products")]
    public class ApiProductsWrapperViewModel
    {
        [XmlElement("Product")]
        public List<ApiProductViewModel> Products { get; set; }
    }
}