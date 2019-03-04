using Microsoft.EntityFrameworkCore;
using KingPim.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace KingPim.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Fick fel utan raden kod nedan, hittade hjälp här https://stackoverflow.com/questions/40703615/the-entity-type-identityuserloginstring-requires-a-primary-key-to-be-defined/40824620
            base.OnModelCreating(builder);

            builder.Entity<ProductCharacteristicValue>().HasKey(table => new {
                table.ProductId,
                table.CharacteristicId
            });
            builder.Entity<SubCategoryCharacteristicGroup>().HasKey(table => new {
                table.SubCategoryId,
                table.CharacteristicGroupId
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<CharacteristicGroup> CharacteristicGroups { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<CharacteristicType> CharacteristicTypes { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<MediaType> MediaTypes { get; set; }
        public DbSet<ProductCharacteristicValue> ProductCharacteristicValues { get; set; }
        public DbSet<SubCategoryCharacteristicGroup> SubCategoryCharacteristicGroups { get; set; }
        public DbSet<CharacteristicTypeSelectList> CharacteristicTypeSelectLists { get; set; }
    }
}