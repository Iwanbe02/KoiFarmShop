using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Order? Order { get; set; }
}
