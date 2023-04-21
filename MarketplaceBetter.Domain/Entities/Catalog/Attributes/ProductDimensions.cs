using MarketplaceBetter.Domain.Entities;
using MarketplaceBetter.Domain.Entities.Catalog.ColorsAndSizes;
using MarketplaceBetter.Domain.Entities.Catalog.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Catalog.Attributes
{
    public class ProductDimensions : Entity
    {
        public long ProductId { get; set; }

        public virtual Product Product { get; set; }

        public long SizeId { get; set; }

        public virtual Size Size { get; set; }

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
