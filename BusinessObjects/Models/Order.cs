using BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order : ISoftDelete
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? KoiFishyId { get; set; }

    public int? AccountId { get; set; }

    public int? PaymentId { get; set; }

    public string? Status { get; set; }

    public bool? Type { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual KoiFish? Koi { get; set; }

    public virtual KoiFishy? KoiFishy { get; set; }

    public virtual Payment? Payment { get; set; }
}
