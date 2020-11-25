namespace TCRMDataManager.Library.Models
{
    public class SaleDetailPost
    {
        public int Id { get; set; }

        public int Product { get; set; }

        public int Quantity { get; set; }
    }

    public class SaleDetail : SaleDetailPost
    {
        public virtual Sale Sale { get; set; }

        public new Product Product { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Tax { get; set; }
    }
}
