using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Cart
{
    public int Id { get; set; }

    public decimal? Price { get; set; }

    public decimal? TotalPrice { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Order? Order { get; set; }
}
