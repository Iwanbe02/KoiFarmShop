using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Consignment
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? KoiCode { get; set; }

    public string? Name { get; set; }

    public int? YearOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Origin { get; set; }

    public string? Variety { get; set; }

    public string? Character { get; set; }

    public double? Size { get; set; }

    public double? AmountFood { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
