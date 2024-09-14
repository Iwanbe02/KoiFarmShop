using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public int Point { get; set; }

    public double DiscountPercentage { get; set; }

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }
}
