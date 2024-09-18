using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class OriginCertificate 
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? OrderId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual KoiFish? Koi { get; set; }
}
