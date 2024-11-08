using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models;

public partial class Consignment
{
   
    public int Id { get; set; }

    public int? AccountId { get; set; }

 
    public int? KoiId { get; set; }

   
    public int? PaymentId { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual KoiFish? Koi { get; set; }

    public virtual Payment? Payment { get; set; }
}
