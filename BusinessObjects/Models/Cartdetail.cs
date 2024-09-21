using BusinessObjects.Helpers;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class CartDetail : ISoftDelete
{
    public int Id { get; set; }

    public int? KoiId { get; set; }

    public int? CartId { get; set; }

    public int? FeedbackId { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Feedback? Feedback { get; set; }

    public virtual KoiFish? Koi { get; set; }
}
