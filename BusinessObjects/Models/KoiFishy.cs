using BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFishy : ISoftDelete
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
