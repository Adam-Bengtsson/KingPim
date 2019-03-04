using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace KingPim.Web.ViewModels
{
    public class ApiProductCharacteristicValuesViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
