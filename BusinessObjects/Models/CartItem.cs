using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public int? KoiFishId { get; set; }

    public int? KoiFishyId { get; set; }

    public int? ConsignmentId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Consignment? Consignment { get; set; }

    public virtual KoiFish? KoiFish { get; set; }

    public virtual KoiFishy? KoiFishy { get; set; }
}
