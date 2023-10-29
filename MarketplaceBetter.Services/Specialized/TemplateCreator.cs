using CsvHelper.Configuration;
using CsvHelper;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Amazon.Inventory;
using MarketplaceBetter.Domain.Entities.Base;
using MarketplaceBetter.Services.Specialized.Interfaces;
using MarketplaceBetter.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes.Size;
using Variant = MarketplaceBetter.Domain.Entities.Catalog.Products.Variant;
using MarketplaceBetter.Domain.Entities.Catalog.Attributes;
using MarketplaceBetter.Domain.Entities.Amazon.CopyAndMedia;
using MarketplaceBetter.Services.Domain.Amazon.CopyAndMedia.Interfaces;
using MarketplaceBetter.Domain.Model.Amazon.CopyAndMedia;

namespace MarketplaceBetter.Services.Specialized
{
    public class TemplateCreator : ITemplateCreator
    {
        private readonly IRepository<Parent> _parentRepository;
        private readonly IRepository<Child> _childRepository;
        private readonly IRepository<ColorTranslation> _colorTranslationRepository;
        private readonly IRepository<ProductDimensions> _productDimensionsRepository;
        private readonly IPhotoService _photoService;
        private readonly ICopywritingService _copywritingService;

        public TemplateCreator(
            IUnitOfWork unitOfWork,
            IPhotoService photoService,
            ICopywritingService copywritingService)
        {
            _parentRepository = unitOfWork.GetRepository<Parent>();
            _childRepository = unitOfWork.GetRepository<Child>();
            _colorTranslationRepository = unitOfWork.GetRepository<ColorTranslation>();
            _productDimensionsRepository = unitOfWork.GetRepository<ProductDimensions>();
            _photoService = photoService;
            _copywritingService = copywritingService;
        }

        public Stream CreateTemplateFor(long parentId)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = false,
            };

            CsvWriter csv = new CsvWriter(writer, config);
            Parent parent = _parentRepository.Get(parentId);

            WriteHeader(parent, csv);
            WriteParent(parent, csv);

            IList<Child> childs = _childRepository.Where(c =>
                c.Variant.ProductId == parent.ProductId && c.InstanceId == parent.InstanceId && c.Status.SystemName != EntityStatusEnum.Withdrawn).OrderByDescending(c => c.Variant.Color.Name).ThenByDescending(c => c.Variant.Size.Name).ToList();
            foreach (var child in childs)
            {
                WriteChild(child, csv);
            }

            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private void WriteHeader(Parent parent, CsvWriter csv)
        {
            csv.WriteField("Seller SKU");
            csv.WriteField("Brand Name");
            csv.WriteField("Product Name");
            csv.WriteField("Product ID");
            csv.WriteField("Color Name");
            csv.WriteField("Color Map");
            csv.WriteField("Size Name");
            // copywriting
            csv.WriteField("Description");
            csv.WriteField("Bullet Point 1");
            csv.WriteField("Bullet Point 2");
            csv.WriteField("Bullet Point 3");
            csv.WriteField("Bullet Point 4");
            csv.WriteField("Bullet Point 5");
            // dimensions
            if (_productDimensionsRepository.Any(d => d.ProductId == parent.ProductId))
            {
                string unit = parent.Instance.SystemName == InstanceEnum.US ? "[in]" : "[cm]";
                csv.WriteField($"Depth {unit}");
                csv.WriteField($"Width {unit}");
                csv.WriteField($"Height {unit}");
                csv.WriteField($"Length {unit}");
            }
            // images
            csv.WriteField("Main Image");
            csv.WriteField("Other Image 1");
            csv.WriteField("Other Image 2");
            csv.WriteField("Other Image 3");
            csv.WriteField("Other Image 4");
            csv.WriteField("Other Image 5");
            csv.WriteField("Other Image 6");
            csv.WriteField("Other Image 7");
            csv.WriteField("Other Image 8");
            csv.WriteField("Swatch Image");
            csv.NextRecord();
        }

        private void WriteParent(Parent parent, CsvWriter csv)
        {
            csv.WriteField(parent.Sku);
            csv.WriteField(parent.Product.Brand.Name);
            csv.WriteField(GetProductName(parent));
            csv.WriteField(parent.Asin);
            csv.WriteField(null);
            csv.WriteField(null);
            csv.WriteField(null);
            WriteCopywriting(csv, parent.ProductId, parent.InstanceId);
            csv.NextRecord();
        }

        private void WriteChild(Child child, CsvWriter csv)
        {
            ColorTranslation colorTranslation = _colorTranslationRepository.SingleOrDefault(t =>
                t.ColorId == child.Variant.Color.Id && t.InstanceId == child.InstanceId);

            csv.WriteField(child.Sku);
            csv.WriteField(child.Variant.Product.Brand.Name);
            csv.WriteField(GetProductName(child, colorTranslation));
            csv.WriteField(child.Variant.Asin ?? child.Variant.Ean);
            csv.WriteField(colorTranslation?.Translation);
            csv.WriteField(colorTranslation?.Mapping);
            csv.WriteField(child.Variant.Size.Name);
            WriteCopywriting(csv, child.Variant.ProductId, child.InstanceId);
            WriteProductDimensions(csv, child);
            WritePhotos(csv, child);
            csv.NextRecord();
        }

        private void WriteCopywriting(CsvWriter csv, long productId, long instanceId)
        {
            CopywritingModel description = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.Description);
            csv.WriteField(description?.Value);
            CopywritingModel bulletPoint1 = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.BulletPoint1);
            csv.WriteField(bulletPoint1?.Value);
            CopywritingModel bulletPoint2 = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.BulletPoint2);
            csv.WriteField(bulletPoint2?.Value);
            CopywritingModel bulletPoint3 = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.BulletPoint3);
            csv.WriteField(bulletPoint3?.Value);
            CopywritingModel bulletPoint4 = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.BulletPoint4);
            csv.WriteField(bulletPoint4?.Value);
            CopywritingModel bulletPoint5 = _copywritingService.GetForProduct(productId, instanceId, CopywritingElementEnum.BulletPoint5);
            csv.WriteField(bulletPoint5?.Value);
        }

        private void WriteProductDimensions(CsvWriter csv, Child child)
        {
            if (_productDimensionsRepository.Any(d => d.ProductId == child.Variant.ProductId))
            {
                ProductDimensions dimensions = _productDimensionsRepository.SingleOrDefault(d => d.ProductId == child.Variant.ProductId && d.SizeId == child.Variant.SizeId);
                if (dimensions == null)
                {
                    csv.WriteField(null);
                    csv.WriteField(null);
                    csv.WriteField(null);
                    csv.WriteField(null);

                    return;
                }

                if (child.Instance.SystemName == InstanceEnum.US)
                {
                    csv.WriteField(dimensions.DepthInInches);
                    csv.WriteField(dimensions.WidthInInches);
                    csv.WriteField(dimensions.HeightInInches);
                    csv.WriteField(dimensions.LengthInInches);
                }
                else
                {
                    csv.WriteField(dimensions.DepthInCentimeters);
                    csv.WriteField(dimensions.WidthInCentimeters);
                    csv.WriteField(dimensions.HeightInCentimeters);
                    csv.WriteField(dimensions.LengthInCentimeters);
                }
            }
        }

        private void WritePhotos(CsvWriter csv, Child child)
        {
            PhotoModel main = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Main, InstanceEnum.All);
            csv.WriteField(main?.Url);
            PhotoModel other1 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other1, InstanceEnum.All);
            csv.WriteField(other1?.Url);
            PhotoModel other2 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other2, InstanceEnum.All);
            csv.WriteField(other2?.Url);
            PhotoModel other3 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other3, InstanceEnum.All);
            csv.WriteField(other3?.Url);
            PhotoModel other4 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other4, InstanceEnum.All);
            csv.WriteField(other4?.Url);
            PhotoModel other5 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other5, InstanceEnum.All);
            csv.WriteField(other5?.Url);
            PhotoModel other6 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other6, InstanceEnum.All);
            csv.WriteField(other6?.Url);
            PhotoModel other7 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other7, InstanceEnum.All);
            csv.WriteField(other7?.Url);
            PhotoModel other8 = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Other8, InstanceEnum.All);
            csv.WriteField(other8?.Url);
            PhotoModel swatch = _photoService.GetForVariant(child.Variant.Id, PhotoTypeEnum.Swatch, InstanceEnum.All);
            csv.WriteField(swatch?.Url);
        }

        private string GetProductName(Parent parent)
        {
            CopywritingModel title = _copywritingService.GetForProduct(parent.ProductId, parent.InstanceId, CopywritingElementEnum.Title);

            return title?.Value;
        }

        private string GetProductName(Child child, ColorTranslation colorTranslation)
        {
            Variant variant = child.Variant;
            Size size = variant.Size;
            CopywritingModel title = _copywritingService.GetForProduct(variant.ProductId, child.InstanceId, CopywritingElementEnum.Title);

            if (size.IsOneSize)
            {
                return $"{title?.Value} ({colorTranslation?.Translation})";
            }
            else
            {
                return $"{title?.Value} ({size.Code}, {colorTranslation?.Translation})";
            }
        }
    }
}
