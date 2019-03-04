using KingPim.DataAccess;
using KingPim.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KingPim.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            ctx = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Product> Products => ctx.Products;
        public IEnumerable<Category> Categories => ctx.Categories;
        public IEnumerable<SubCategory> SubCategories => ctx.SubCategories;
        public IEnumerable<CharacteristicGroup> CharacteristicGroups => ctx.CharacteristicGroups;
        public IEnumerable<Characteristic> Characteristics => ctx.Characteristics;
        public IEnumerable<CharacteristicType> CharacteristicTypes => ctx.CharacteristicTypes;
        public IEnumerable<CharacteristicTypeSelectList> CharacteristicTypeSelectLists => ctx.CharacteristicTypeSelectLists;
        public IEnumerable<Media> Medias => ctx.Medias;
        public IEnumerable<MediaType> MediaTypes => ctx.MediaTypes;
        public IEnumerable<SubCategoryCharacteristicGroup> SubCategoryCharacteristicGroups => ctx.SubCategoryCharacteristicGroups;
        public IEnumerable<ProductCharacteristicValue> ProductCharacteristicValues => ctx.ProductCharacteristicValues;

        public void SaveCategory(Category c)
        {
            if (c.Id == 0)
            {
                c.CreatedDate = DateTime.Now;
                c.UpdatedDate = DateTime.Now;
                c.Version = 1;
                c.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                ctx.Categories.Add(c);
                ctx.SaveChanges();
            }
            else
            {
                var ctxCategory = ctx.Categories.FirstOrDefault(x => x.Id.Equals(c.Id));
                if (ctxCategory != null)
                {
                    ctxCategory.Name = c.Name;
                    ctxCategory.UpdatedDate = DateTime.Now;
                    ctxCategory.Version = ++c.Version;
                    ctxCategory.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteCategory(int categoryId)
        {
            var ctxCategory = ctx.Categories.FirstOrDefault(x => x.Id.Equals(categoryId));
            if (ctxCategory != null)
            {
                ctx.Categories.Remove(ctxCategory);
                ctx.SaveChanges();
            }
            return $"Succé! Kategorin \"{ctxCategory?.Name}\" togs bort";
        }

        public string PublishCategory(int categoryId, bool changePublishedValueTo)
        {
            var ctxCategory = ctx.Categories.FirstOrDefault(x => x.Id.Equals(categoryId));
            if (ctxCategory != null)
            {
                ctxCategory.Published = changePublishedValueTo;
                ctx.SaveChanges();
            }
            return $"Succé! Publiceringsstatus på kategorin \"{ctxCategory?.Name}\" har ändrats";
        }

        public void SaveSubCategory(SubCategory s)
        {
            if (s.Id == 0)
            {
                s.CreatedDate = DateTime.Now;
                s.UpdatedDate = DateTime.Now;
                s.Version = 1;
                s.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                ctx.SubCategories.Add(s);
                ctx.SaveChanges();
            }
            else
            {
                var ctxSubCategory = ctx.SubCategories.FirstOrDefault(x => x.Id.Equals(s.Id));
                if (ctxSubCategory != null)
                {
                    ctxSubCategory.Name = s.Name;
                    ctxSubCategory.UpdatedDate = DateTime.Now;
                    ctxSubCategory.Version = ++s.Version;
                    ctxSubCategory.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    ctxSubCategory.CategoryId = s.CategoryId;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteSubCategory(int subCategoryId)
        {
            var ctxSubCategory = ctx.SubCategories.FirstOrDefault(x => x.Id.Equals(subCategoryId));
            if (ctxSubCategory != null)
            {
                ctx.SubCategories.Remove(ctxSubCategory);
                ctx.SaveChanges();
            }
            return $"Succé! Underkategorin \"{ctxSubCategory?.Name}\" togs bort";
        }

        public string PublishSubCategory(int SubCategoryId, bool changePublishedValueTo)
        {
            var ctxSubCategory = ctx.SubCategories.FirstOrDefault(x => x.Id.Equals(SubCategoryId));
            if (ctxSubCategory != null)
            {
                ctxSubCategory.Published = changePublishedValueTo;
                ctx.SaveChanges();
            }
            return $"Succé! Publiceringsstatus på underkategorin \"{ctxSubCategory?.Name}\" har ändrats";
        }

        public void SaveCharacteristicGroup(CharacteristicGroup c)
        {
            if (c.Id == 0)
            {
                c.CreatedDate = DateTime.Now;
                c.UpdatedDate = DateTime.Now;
                c.Version = 1;
                c.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                ctx.CharacteristicGroups.Add(c);
                ctx.SaveChanges();
            }
            else
            {
                var ctxCharacteristicGroup = ctx.CharacteristicGroups.FirstOrDefault(x => x.Id.Equals(c.Id));
                if (ctxCharacteristicGroup != null)
                {
                    ctxCharacteristicGroup.Name = c.Name;
                    ctxCharacteristicGroup.UpdatedDate = DateTime.Now;
                    ctxCharacteristicGroup.Version = ++c.Version;
                    ctxCharacteristicGroup.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteCharacteristicGroup(int characteristicGroupId)
        {
            var ctxCharacteristicGroup = ctx.CharacteristicGroups.FirstOrDefault(x => x.Id.Equals(characteristicGroupId));
            if (ctxCharacteristicGroup != null)
            {
                ctx.CharacteristicGroups.Remove(ctxCharacteristicGroup);
                ctx.SaveChanges();
            }
            return $"Succé! Attributgruppen \"{ctxCharacteristicGroup?.Name}\" togs bort";
        }

        public void SaveCharacteristic(Characteristic c)
        {
            if (c.Id == 0)
            {
                c.CreatedDate = DateTime.Now;
                c.UpdatedDate = DateTime.Now;
                c.Version = 1;
                c.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                ctx.Characteristics.Add(c);
                ctx.SaveChanges();
            }
            else
            {
                var ctxCharacteristic = ctx.Characteristics.FirstOrDefault(x => x.Id.Equals(c.Id));
                if (ctxCharacteristic != null)
                {
                    ctxCharacteristic.Name = c.Name;
                    ctxCharacteristic.UpdatedDate = DateTime.Now;
                    ctxCharacteristic.Version = ++c.Version;
                    ctxCharacteristic.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    ctxCharacteristic.CharacteristicGroupId = c.CharacteristicGroupId;
                    ctxCharacteristic.CharacteristicTypeId = c.CharacteristicTypeId;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteCharacteristic(int characteristicId)
        {
            var ctxCharacteristic = ctx.Characteristics.FirstOrDefault(x => x.Id.Equals(characteristicId));
            if (ctxCharacteristic != null)
            {
                ctx.Characteristics.Remove(ctxCharacteristic);
                ctx.SaveChanges();
            }
            return $"Succé! Attributet \"{ctxCharacteristic?.Name}\" togs bort";
        }

        public void DeleteSubCategoryCharacteristicGroups(int subCategoryId)
        {
            foreach (var subCategoryCharacteristicGroup in ctx.SubCategoryCharacteristicGroups.Where(x => x.SubCategoryId.Equals(subCategoryId)))
            {
                ctx.SubCategoryCharacteristicGroups.Remove(subCategoryCharacteristicGroup);
            }
            ctx.SaveChanges();
        }

        public void AddSubCategoryCharacteristicGroup(int subCategoryId, int characteristicGroupId)
        {
            var subCategoryCharacteristicGroup = new SubCategoryCharacteristicGroup
            {
                SubCategoryId = subCategoryId,
                CharacteristicGroupId = characteristicGroupId
            };
            ctx.SubCategoryCharacteristicGroups.Add(subCategoryCharacteristicGroup);
            ctx.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            if (p.Id == 0)
            {
                p.CreatedDate = DateTime.Now;
                p.UpdatedDate = DateTime.Now;
                p.Version = 1;
                p.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                ctx.Products.Add(p);
                ctx.SaveChanges();
            }
            else
            {
                var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(p.Id));
                if (ctxProduct != null)
                {
                    ctxProduct.Name = p.Name;
                    ctxProduct.UpdatedDate = DateTime.Now;
                    ctxProduct.Version = ++p.Version;
                    ctxProduct.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
                    ctxProduct.SubCategoryId = p.SubCategoryId;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteProduct(int productId)
        {
            var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(productId));
            if (ctxProduct != null)
            {
                ctx.Products.Remove(ctxProduct);
                ctx.SaveChanges();
            }
            return $"Succé! Produkten \"{ctxProduct?.Name}\" togs bort";
        }

        public string PublishProduct(int productId, bool changePublishedValueTo)
        {
            var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(productId));
            if (ctxProduct != null)
            {
                ctxProduct.Published = changePublishedValueTo;
                ctx.SaveChanges();
            }
            return $"Succé! Publiceringsstatus på produkten \"{ctxProduct?.Name}\" har ändrats";
        }

        public void SaveProductCharacteristicValue(ProductCharacteristicValue p)
        {
            ctx.ProductCharacteristicValues.Add(p);
            ctx.SaveChanges();
        }

        public void DeleteProductCharacteristicValues(int productId)
        {
            // Koden på följande 7 rader nedan ser till att systemspecifika read-only uppdateras på produkter
            var ctxProduct = ctx.Products.FirstOrDefault(x => x.Id.Equals(productId));
            if (ctxProduct != null)
            {
                ++ctxProduct.Version;
                ctxProduct.UpdatedDate = DateTime.Now;
                ctxProduct.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
            }
            foreach (var productCharacteristicValue in ctx.ProductCharacteristicValues.Where(x => x.ProductId.Equals(productId)))
            {
                ctx.ProductCharacteristicValues.Remove(productCharacteristicValue);
            }
            ctx.SaveChanges();
        }

        public void SaveCharacteristicType(CharacteristicType c)
        {
            if (c.Id == 0)
            {
                ctx.CharacteristicTypes.Add(c);
                ctx.SaveChanges();
            }
            else
            {
                var ctxCharacteristicType = ctx.CharacteristicTypes.FirstOrDefault(x => x.Id.Equals(c.Id));
                if (ctxCharacteristicType != null)
                {
                    ctxCharacteristicType.Name = c.Name;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteCharacteristicType(int characteristicTypeId)
        {
            var ctxCharacteristicType = ctx.CharacteristicTypes.FirstOrDefault(x => x.Id.Equals(characteristicTypeId));
            if (ctxCharacteristicType != null)
            {
                ctx.CharacteristicTypes.Remove(ctxCharacteristicType);
                ctx.SaveChanges();
            }
            return $"Succé! Attributlistan \"{ctxCharacteristicType?.Name}\" togs bort";
        }

        public void SaveCharacteristicTypeSelectList(CharacteristicTypeSelectList c)
        {
            if (c.Id == 0)
            {
                ctx.CharacteristicTypeSelectLists.Add(c);
                ctx.SaveChanges();
            }
            else
            {
                var ctxCharacteristicTypeSelectList = ctx.CharacteristicTypeSelectLists.FirstOrDefault(x => x.Id.Equals(c.Id));
                if (ctxCharacteristicTypeSelectList != null)
                {
                    ctxCharacteristicTypeSelectList.Value = c.Value;
                    ctx.SaveChanges();
                }
            }
        }

        public string DeleteCharacteristicTypeSelectList(int characteristicTypeSelectListId)
        {
            var ctxCharacteristicTypeSelectList = ctx.CharacteristicTypeSelectLists.FirstOrDefault(x => x.Id.Equals(characteristicTypeSelectListId));
            if (ctxCharacteristicTypeSelectList != null)
            {
                ctx.CharacteristicTypeSelectLists.Remove(ctxCharacteristicTypeSelectList);
                ctx.SaveChanges();
            }
            return $"Succé! Attributlista-attributet \"{ctxCharacteristicTypeSelectList?.Value}\" togs bort";
        }
    }
}