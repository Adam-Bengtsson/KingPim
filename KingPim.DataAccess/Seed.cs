using KingPim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingPim.DataAccess
{
    public static class Seed
    {
        public static void FillIfEmpty(ApplicationDbContext ctx)
        {
            if (!ctx.Categories.Any())
            {
                ctx.Categories.Add(new Category{ Name = "Kamera", Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.Categories.Add(new Category{ Name = "TV", Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.SaveChanges();
            }

            if (!ctx.SubCategories.Any())
            {
                ctx.SubCategories.Add(new SubCategory { Name = "Kompaktkamera", CategoryId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.SubCategories.Add(new SubCategory { Name = "Systemkamera", CategoryId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.SubCategories.Add(new SubCategory { Name = "OLED", CategoryId = 2, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.SaveChanges();
            }

            if (!ctx.Products.Any())
            {
                ctx.Products.Add(new Product { ProductId = 1, Name = "Canon EOS 5D Mark IV", SubCategoryId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.Products.Add(new Product { ProductId = 2, Name = "Sony Alpha A7 III", SubCategoryId = 2, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.Products.Add(new Product { ProductId = 2, Name = "LG OLED55C7V", SubCategoryId = 3, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed", Published = true });
                ctx.SaveChanges();
            }

            if (!ctx.CharacteristicGroups.Any())
            {
                ctx.CharacteristicGroups.Add(new CharacteristicGroup { Name = "Dimensioner", Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed" });
                ctx.SaveChanges();
            }

            if (!ctx.CharacteristicTypes.Any())
            {
                ctx.CharacteristicTypes.Add(new CharacteristicType { Name = "Text" });
                ctx.CharacteristicTypes.Add(new CharacteristicType { Name = "Sant/Falskt" });
                ctx.CharacteristicTypes.Add(new CharacteristicType { Name = "USB-versioner" });
                ctx.SaveChanges();
            }

            if (!ctx.Characteristics.Any())
            {
                ctx.Characteristics.Add(new Characteristic { Name = "Längd", CharacteristicGroupId = 1, CharacteristicTypeId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed" });
                ctx.Characteristics.Add(new Characteristic { Name = "Bredd", CharacteristicGroupId = 1, CharacteristicTypeId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed" });
                ctx.Characteristics.Add(new Characteristic { Name = "Höjd", CharacteristicGroupId = 1, CharacteristicTypeId = 1, Version = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, UpdatedBy = "Seed" });
                ctx.SaveChanges();
            }

            if (!ctx.CharacteristicTypeSelectLists.Any())
            {
                ctx.CharacteristicTypeSelectLists.Add(new CharacteristicTypeSelectList { CharacteristicTypeId = 3, Value = "Version 2" });
                ctx.CharacteristicTypeSelectLists.Add(new CharacteristicTypeSelectList { CharacteristicTypeId = 3, Value = "Version 3" });
                ctx.CharacteristicTypeSelectLists.Add(new CharacteristicTypeSelectList { CharacteristicTypeId = 3, Value = "Typ C" });
                ctx.SaveChanges();
            }
        }
    }
}
