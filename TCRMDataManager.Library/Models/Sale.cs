using System;
using System.Collections.Generic;

namespace TCRMDataManager.Library.Models
{
    public class SalePost
    {
        public int Id { get; set; }

        public List<SaleDetailPost> SaleDetails { get; set; }
    }

    public class Sale : SalePost
    {
        public virtual User User { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

        public new virtual List<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

        public Sale() { }
    }
}
