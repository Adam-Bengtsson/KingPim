using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KingPim.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infrastructure
{
    public static class CategoryExtensions
    {
        public static List<SelectListItem> ToSelectList(this IEnumerable<Category> categories, Category c = null)
        {
            var list = categories.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == c?.Id)
                }).ToList();
            return list;
        }
        //public static List<SelectListItem> ToSelectList(this IEnumerable<Dealership> dealerships, Vehicle v = null)
        //{
        //    var list = dealerships.Select(
        //        x => new SelectListItem
        //        {
        //            Text = x.City,
        //            Value = x.Id.ToString(),
        //            Selected = (x.Id == v?.DealerShipId)

        //        }).ToList();

        //    return list;


        //}
    }
}
