using System;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBetter.Domain.Model.Sales.InputData
{
    public class DownloadSalesReportModel
    {
        [Required]
        [Display(Name = "From")]
        public DateTime? DateFrom { get; set; }

        [Required]
        [Display(Name = "To")]
        public DateTime? DateTo { get; set; }
    }
}
