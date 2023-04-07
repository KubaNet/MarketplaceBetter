using MarketplaceBetter.Domain.Entities.Amazon.Campaigns;
using MarketplaceBetter.Domain.Entities.Catalog.CopyAndMedia;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Amazon.Keywords;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceBetter.Domain.Entities.Base;

namespace MarketplaceBetter.Infrastructure.Data
{
    public class BetterDbContext : DbContext, IDbContext
    {
        public BetterDbContext(DbContextOptions<BetterDbContext> options) : base(options)
        {
            // Disable initializer
            //Database.SetInitializer<MarketplaceBetterDbContext>(null);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLazyLoadingProxies();

        // Base
        public DbSet<Instance> Instance { get; set; }
        public DbSet<EntityStatus> EntityStatus { get; set; }

        // Catalog
        // => Products
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Collection> Collection { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Variant> Variant { get; set; }
        // => Colors and Sizes
        public DbSet<Color> Color { get; set; }
        public DbSet<ColorTranslation> ColorTranslation { get; set; }
        public DbSet<ColorGroup> ColorGroup { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<SizeGroup> SizeGroup { get; set; }
        // => CopyAndMedia
        public DbSet<Copywriting> Copywriting { get; set; }
        public DbSet<CopywritingElement> CopywritingElement { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<PhotoType> PhotoType { get; set; }
        public DbSet<PhotoKind> PhotoKind { get; set; }
        public DbSet<PhotoKindBeginning> PhotoKindBeginning { get; set; }
        public DbSet<PhotoUpload> PhotoUpload { get; set; }
        public DbSet<PhotoUploadVariant> PhotoUploadVariant { get; set; }

        // Amazon
        // => Inventory
        public DbSet<ParentInstance> ParentInstance { get; set; }
        public DbSet<ChildInstance> ChildInstance { get; set; }
        // => Campaigns
        public DbSet<Campaign> Campaign { get; set; }
        public DbSet<CampaignType> CampaignType { get; set; }
        public DbSet<CampaignStrategy> CampaignStrategy { get; set; }
        public DbSet<AdEntityStatus> AdEntityStatus { get; set; }
        public DbSet<AdGroup> AdGroup { get; set; }
        public DbSet<ProductAd> ProductAd { get; set; }
        public DbSet<MatchType> MatchType { get; set; }
        public DbSet<NegativeKeyword> NegativeKeyword { get; set; }
        public DbSet<NegativeProduct> NegativeProduct { get; set; }
        public DbSet<AmazonTargetingStatus> AmazonTargetingStatus { get; set; }
        public DbSet<AmazonTargeting> AmazonTargeting { get; set; }
        // => Keywords
        public DbSet<Research> Research { get; set; }
        public DbSet<ResearchTarget> ResearchTarget { get; set; }
        public DbSet<ResearchTargetSource> ResearchTargetSource { get; set; }
        public DbSet<ResearchTargetStatus> ResearchTargetStatus { get; set; }
        public DbSet<ResearchResult> ResearchResult { get; set; }

        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        // Retrieve the error messages as a list of strings.
        //        var errorMessages = ex.EntityValidationErrors
        //                .SelectMany(x => x.ValidationErrors)
        //                .Select(x => x.ErrorMessage);

        //        // Join the list to a single string.
        //        var fullErrorMessage = string.Join("; ", errorMessages);

        //        // Combine the original exception message with the new one.
        //        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

        //        // Throw a new DbEntityValidationException with the improved exception message.
        //        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
        //    }
        //}

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    if (modelBuilder == null)
        //    {
        //        throw new ArgumentNullException("modelBuilder");
        //    }

        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    modelBuilder.Conventions.Add<ForeignKeyNamingConvention>();

        //    // domyślnie wszystkie pola zwykłe (nie relacyjne, ale także string) są wymagane
        //    modelBuilder.Properties().Configure(p => p.IsRequired());

        //    // typy nullable nie są wymagane
        //    modelBuilder.Properties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)).Configure(p => p.IsOptional());

        //    // domyślnie kolumny typu string mają długość max 255 znaków
        //    modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(255));

        //    modelBuilder.Properties().Where(p => p.Name.Equals("RowVersion")).Configure(p => p.IsRowVersion());

        //    //modelBuilder.Configurations.Add(new UserConfiguration());
        //}

        //protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        //{
        //    DbEntityValidationResult validationResult = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

        //    ValidateDateTimeValues(entityEntry, validationResult);
        //    ValidateEnumValues(entityEntry, validationResult);

        //    if (validationResult.ValidationErrors.Any())
        //    {
        //        return validationResult;
        //    }
        //    else
        //    {
        //        return base.ValidateEntity(entityEntry, items);
        //    }
        //}

        //// pole typu DateTime nie może mieć wartości DateTime.Min (domyślnej)
        //private void ValidateDateTimeValues(DbEntityEntry entityEntry, DbEntityValidationResult validationResult)
        //{
        //    Type entityType = entityEntry.Entity.GetType();

        //    foreach (var dateTimeProperty in entityType.GetProperties().Where(p => p.PropertyType == typeof(DateTime)))
        //    {
        //        DateTime value = (DateTime)dateTimeProperty.GetValue(entityEntry.Entity);

        //        if (value == DateTime.MinValue)
        //        {
        //            validationResult.ValidationErrors.Add(new DbValidationError(dateTimeProperty.Name, "Value can't be DateTime.Min (default)."));
        //        }
        //    }
        //}

        //// pole typu Enum nie może mieć wartości None (0 - domyślnej)
        //private void ValidateEnumValues(DbEntityEntry entityEntry, DbEntityValidationResult validationResult)
        //{
        //    Type entityType = entityEntry.Entity.GetType();

        //    foreach (var enumProperty in entityType.GetProperties().Where(p => p.PropertyType.IsEnum))
        //    {
        //        int value = (int)enumProperty.GetValue(entityEntry.Entity);

        //        if (value == 0)
        //        {
        //            validationResult.ValidationErrors.Add(new DbValidationError(enumProperty.Name, "Value can't be None (default)."));
        //        }
        //    }
        //}
    }
}