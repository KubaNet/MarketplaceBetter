using MarketplaceBetter.Domain.Model.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Model.Catalog.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Model.Catalog.Attributes
{
    public class ProductDimensionsModel
    {
        public long Id { get; set; }

        [Required]
        public ProductModel Product { get; set; }

        [Required]
        public SizeModel Size { get; set; }

        public double? WeightInGrams { get; set; }

        public double? WeightInPounds { get; set; }

        public double? DepthInCentimeters { get; set; }

        public double? DepthInInches { get; set; }

        public double? LengthInCentimeters { get; set; }

        public double? LengthInInches { get; set; }

        public double? WidthInCentimeters { get; set; }

        public double? WidthInInches { get; set; }

        public double? HeightInCentimeters { get; set; }

        public double? HeightInInches { get; set; }
    }
}
