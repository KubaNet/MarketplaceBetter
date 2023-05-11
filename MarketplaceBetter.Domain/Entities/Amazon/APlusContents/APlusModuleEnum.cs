using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBetter.Domain.Entities.Amazon.APlusContents
{
    public enum APlusModuleEnum
    {
        None = 0,

        StandardComparisonChart = 1,

        StandardFourImagesAndText = 2,

        StandardFourImagesWithTextQuadrant = 3,

        StandardImageAndDarkTextOverlay = 4,

        StandardImageAndLightTextOverlay = 5,

        StandardImageHeaderWithText = 6,

		StandardSingleImageAndHighlights = 7,

		StandardSingleImageAndSidebar = 8,

		StandardSingleImageAndSpecsDetail = 9,

		StandardSingleLeftImage = 10,

		StandardSingleRightImage = 11,

		StandardText = 12,

		StandardThreeImagesAndText = 13,

		StandardCompanyLogo = 14,

		StandardMultipleImageModuleA = 15,

		StandardProductDescriptionText = 16,

		StandardTechnicalSpecifications = 17
	}
}
