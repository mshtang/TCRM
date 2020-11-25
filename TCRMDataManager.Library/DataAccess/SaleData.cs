using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(Sale saleInfo, string cashierId)
        {
            List<SaleDetailDb> details = new List<SaleDetailDb>();

            var productData = new ProductData();

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDb
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = productData.GetProductById(item.ProductId);
                detail.PurchasePrice = productInfo.RetailPrice * item.Quantity;
                detail.Tax = detail.PurchasePrice * productInfo.Tax.TaxRate;

                details.Add(detail);
            }

            SaleDb sale = new SaleDb
            {
                Subtotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.Subtotal + sale.Tax;

            var context = new TCRMContext("TCRMData");
            context.Sales.Add(sale);
            context.SaveChanges();

            sale.Id = context.Sales.Where(s => s.CashierId == sale.CashierId && s.SaleDate == sale.SaleDate).FirstOrDefault().Id;

            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                context.SaleDetails.Add(item);
            }

            context.SaveChanges();
        }

    }
}
