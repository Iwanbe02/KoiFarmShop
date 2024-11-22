using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public int? AccountId { get; set; }

    public int? PaymentId { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Feedback? Feedback { get; set; }

    public virtual Payment? Payment { get; set; }
}
