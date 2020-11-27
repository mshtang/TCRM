namespace TCRMDataManager.Library.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public decimal RetailPrice { get; set; }

        public virtual TaxCategory Tax { get; set; }

        public int QuantityInStock { get; set; }

        public Product() { }
    }
}
