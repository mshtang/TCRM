using System;

namespace TCRMDataManager.Library.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
