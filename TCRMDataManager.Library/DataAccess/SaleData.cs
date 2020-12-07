using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SalePost salePost, string userId)
        {
            var sale = new Sale();

            using (var context = new TCRMContext("TCRMData"))
            {
                var productIds = salePost.SaleDetails.Select(sd => sd.Product);
                var products = context.Products.Where(p => productIds.Contains(p.Id)).ToList();

                foreach (var saleDetailPost in salePost.SaleDetails)
                {
                    var product = products.Single(p => p.Id == saleDetailPost.Product);
                    var saleDetail = new SaleDetail
                    {
                        Quantity = saleDetailPost.Quantity,
                        Product = product,
                        PurchasePrice = product.RetailPrice * saleDetailPost.Quantity,
                        Tax = product.Tax.TaxRate * saleDetailPost.Quantity
                    };

                    sale.SaleDetails.Add(saleDetail);
                }

                sale.Subtotal = sale.SaleDetails.Sum(x => x.PurchasePrice);
                sale.Tax = sale.SaleDetails.Sum(x => x.Tax);
                sale.Total = sale.Subtotal + sale.Tax;
                sale.User = context.Users.Find(userId);

                context.Sales.Add(sale);
                context.SaveChanges();
            }
        }


        public List<Sale> GetSaleReport()
        {
            using (var context = new TCRMContext("TCRMData"))
            {
                return context.Sales.ToList();
            }
        }
    }
}
