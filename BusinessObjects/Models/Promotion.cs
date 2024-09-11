using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public int Point { get; set; }

    public double DiscountPercentage { get; set; }

    public int CreateBy { get; set; }

    public DateTime CreateDate { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int DeleteBy { get; set; }

    public DateTime DeleteDate { get; set; }

    public bool? IsDelete { get; set; }
}
