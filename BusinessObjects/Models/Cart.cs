using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? KoiFishyId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual KoiFishy? KoiFishy { get; set; }

    public virtual Order? Order { get; set; }
}
