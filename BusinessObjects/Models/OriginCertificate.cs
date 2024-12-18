﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OriginCertificate
{
    public int Id { get; set; }

    public string? Variety { get; set; }

    public string? Gender { get; set; }

    public double? Size { get; set; }

    public int? YearOfBirth { get; set; }

    public DateTime? Date { get; set; }

    public string? PlaceOfIssue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual KoiFish? KoiFish { get; set; }
}
