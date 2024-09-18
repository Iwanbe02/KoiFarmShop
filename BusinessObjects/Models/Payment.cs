﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Payment : BaseEntity
{
    public int Id { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
