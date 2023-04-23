using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public enum APlusSectionTypeEnum
    {
        None = 0,

        StandardComparisonChart = 1,

        StandardFourImagesAndText = 2,

        StandardFourImagesWithTextQuadrant = 3,

        StandardImageAndDarkTextOverlay = 4,

        StandardImageAndLightTextOverlay = 5,

        StandardImageHeaderWithText = 6,
    }
}
