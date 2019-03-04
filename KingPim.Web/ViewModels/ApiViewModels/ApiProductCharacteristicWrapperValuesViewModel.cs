using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    public class ApiProductCharacteristicWrapperValuesViewModel
    {
        [XmlElement("Characteristic")]
        public List<ApiProductCharacteristicValuesViewModel> Characteristics { get; set; }
        public ApiProductCharacteristicWrapperValuesViewModel()
        {
            this.Characteristics = new List<ApiProductCharacteristicValuesViewModel>();
        }
    }
}