using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.ViewModels
{
    public class ChangeUserPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }
    }
}