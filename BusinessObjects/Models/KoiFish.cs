using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class KoiFish
{
    public int KoiId { get; set; }

    public int CategoryId { get; set; }

    public string Origin { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public double Size { get; set; }

    public string Species { get; set; } = null!;

    public string Character { get; set; } = null!;

    public double AmountFood { get; set; }

    public double ScreeningRate { get; set; }

    public int Amount { get; set; }

    public string Type { get; set; } = null!;

    public DateTime? Date { get; set; }

    public bool? IsDelete { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<HealthCertificate> HealthCertificates { get; set; } = new List<HealthCertificate>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<OriginCertificate> OriginCertificates { get; set; } = new List<OriginCertificate>();

    public virtual ICollection<RewardCertificate> RewardCertificates { get; set; } = new List<RewardCertificate>();
}
