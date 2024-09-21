using BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFish : ISoftDelete
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? Origin { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public double? Size { get; set; }

    public string? Species { get; set; }

    public string? Character { get; set; }

    public double? AmountFood { get; set; }

    public double? ScreeningRate { get; set; }

    public string? Type { get; set; }

    public DateTime? Date { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<HealthCertificate> HealthCertificates { get; set; } = new List<HealthCertificate>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<OriginCertificate> OriginCertificates { get; set; } = new List<OriginCertificate>();

    public virtual ICollection<RewardCertificate> RewardCertificates { get; set; } = new List<RewardCertificate>();
}
