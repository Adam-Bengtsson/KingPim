using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KingPim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KingPim.Infrastructure
{
    public static class ToSelectListExtensions
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

        public static List<SelectListItem> ToSelectList(this IEnumerable<SubCategory> subCategories, SubCategory s = null)
        {
            var list = subCategories.Select(

                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == s?.Id)
                }).OrderBy(x => x.Text).ToList();

            return list;
        }

        public static List<SelectListItem> ToSelectListWithCategoryName(this IEnumerable<SubCategory> subCategories, SubCategory s = null)
        {
            var list = subCategories.Select(

                x => new SelectListItem
                {
                    Text = $"{x.Category.Name} / {x.Name}",
                    Value = x.Id.ToString(),
                    Selected = (x.Id == s?.Id)
                }).OrderBy(x => x.Text).ToList();

            return list;
        }

        public static List<SelectListItem> ToSelectList(this IEnumerable<CharacteristicGroup> characteristicGroups, CharacteristicGroup c = null)
        {
            var list = characteristicGroups.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == c?.Id)
                }).ToList();
            return list;
        }

        public static List<SelectListItem> ToSelectList(this IEnumerable<CharacteristicType> characteristicTypes, CharacteristicType c = null)
        {
            var list = characteristicTypes.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == c?.Id)
                }).ToList();
            return list;
        }

        public static List<SelectListItem> ToSelectList(this IEnumerable<IdentityRole> identityRoles, IdentityRole i = null)
        {
            var list = identityRoles.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name,
                    Selected = (x.Name == i?.Name)
                }).ToList();
            return list;
        }
    }
}