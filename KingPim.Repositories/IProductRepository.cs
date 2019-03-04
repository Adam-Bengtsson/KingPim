using KingPim.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingPim.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<SubCategory> SubCategories { get; }
        IEnumerable<CharacteristicGroup> CharacteristicGroups { get;}
        IEnumerable<Characteristic> Characteristics { get; }
        IEnumerable<CharacteristicType> CharacteristicTypes { get; }
        IEnumerable<SubCategoryCharacteristicGroup> SubCategoryCharacteristicGroups { get; }
        IEnumerable<ProductCharacteristicValue> ProductCharacteristicValues { get; }
        IEnumerable<CharacteristicTypeSelectList> CharacteristicTypeSelectLists { get;}

        void SaveProduct(Product p);
        string DeleteProduct(int productId);
        string PublishProduct(int productId, bool changePublishedValueTo);
        void SaveCategory(Category c);
        string DeleteCategory(int categoryId);
        string PublishCategory(int categoryId, bool changePublishedValueTo);
        void SaveSubCategory(SubCategory s);
        string DeleteSubCategory(int subCategoryId);
        string PublishSubCategory(int subCategoryId, bool changePublishedValueTo);
        void SaveCharacteristicGroup(CharacteristicGroup c);
        string DeleteCharacteristicGroup(int characteristicGroupId);
        void SaveCharacteristic(Characteristic c);
        string DeleteCharacteristic(int characteristicId);
        void SaveCharacteristicType(CharacteristicType c);
        string DeleteCharacteristicType(int characteristicTypeId);
        void SaveCharacteristicTypeSelectList(CharacteristicTypeSelectList c);
        string DeleteCharacteristicTypeSelectList(int characteristicTypeSelectListId);

        void DeleteProductCharacteristicValues(int productId);
        void SaveProductCharacteristicValue(ProductCharacteristicValue p);

        void DeleteSubCategoryCharacteristicGroups(int subCategoryId);
        void AddSubCategoryCharacteristicGroup(int subCategoryId, int characteristicGroupId);
    }
}