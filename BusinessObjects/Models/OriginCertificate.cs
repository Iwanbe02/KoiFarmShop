using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OriginCertificate
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public string? Variety { get; set; }

    public string? Gender { get; set; }

    public double? Size { get; set; }

    public int? YearOfBirth { get; set; }

    public DateTime? Date { get; set; }

    public string? Signature { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Image? Image { get; set; }

    public virtual KoiFish? Koi { get; set; }
}
