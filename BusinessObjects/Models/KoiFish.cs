﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFish
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? Origin { get; set; }

    public string? Gender { get; set; }

    public int? YearOfBirth { get; set; }

    public double? Size { get; set; }

    public string? Variety { get; set; }

    public string? Character { get; set; }

    public string? Diet { get; set; }

    public double? AmountFood { get; set; }

    public double? ScreeningRate { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual OriginCertificate? OriginCertificate { get; set; }
}
