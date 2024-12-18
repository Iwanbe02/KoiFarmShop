﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Image
{
    public int Id { get; set; }

    public string? UrlPath { get; set; }

    public int? KoiId { get; set; }

    public int? KoiFishyId { get; set; }

    public int? ConsignmentId { get; set; }

    public int? OriginCertificateId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Consignment? Consignment { get; set; }

    public virtual KoiFish? Koi { get; set; }

    public virtual KoiFishy? KoiFishy { get; set; }

    public virtual OriginCertificate? OriginCertificate { get; set; }
}
