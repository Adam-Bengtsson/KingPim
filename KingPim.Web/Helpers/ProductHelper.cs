using KingPim.Models;
using KingPim.Repositories;
using KingPim.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingPim.Web.Helpers
{
    public class ProductHelper
    {
        private IProductRepository productRepo;

        public ProductHelper(IProductRepository p)
        {
            productRepo = p;
        }

        public ProductCharacteristicValueViewModel EditProductCharacteristicValues(int productId)
        {
            var productSubCategoryId = productRepo.Products.FirstOrDefault(x => x.Id.Equals(productId)).SubCategoryId;
            var productCharacteristicGroupIds = productRepo.SubCategoryCharacteristicGroups.Where(x => x.SubCategoryId.Equals(productSubCategoryId)).Select(x => x.CharacteristicGroupId).ToList();

            var productGroupCharacteristics = new List<CharacteristicGroup>();
            foreach (var productCharacteristicsGroupId in productCharacteristicGroupIds)
            {
                var x = productRepo.CharacteristicGroups.FirstOrDefault(y => y.Id.Equals(productCharacteristicsGroupId));
                productGroupCharacteristics.Add(x);
            }

            var productCharacteristicsIds = new List<int>();
            foreach (var productCharacteristicGroupId in productCharacteristicGroupIds)
            {
                var x = productRepo.Characteristics.Where(y => y.CharacteristicGroupId.Equals(productCharacteristicGroupId)).Select(y => y.Id).ToList();
                productCharacteristicsIds.AddRange(x);
            }

            var productCharacteristics = new List<Characteristic>();
            foreach (var productCharacteristicsId in productCharacteristicsIds)
            {
                var x = productRepo.Characteristics.FirstOrDefault(y => y.Id.Equals(productCharacteristicsId));
                productCharacteristics.Add(x);
            }

            var p = new ProductCharacteristicValueViewModel()
            {
                ProductId = productId,
                CharacteristicGroups = productGroupCharacteristics,
                Characteristics = productCharacteristics,
                ExistingProductCharacteristicValues = productRepo.ProductCharacteristicValues.Where(x => x.ProductId.Equals(productId)),
                CharacteristicTypeSelectLists = productRepo.CharacteristicTypeSelectLists.OrderBy(x => x.Value).ToList()
            };

            return (p);
        }
    }
}