﻿using System.Collections.Generic;
using System.Linq;
using TCRMDataManager.Library.Models;

namespace TCRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SalePost salePost, string userId)
        {
            var sale = new Sale();
            var context = new TCRMContext("TCRMData");
            var productIds = salePost.SaleDetails.Select(sd => sd.Product);
            var products = context.Products.Include("Tax").Where(p => productIds.Contains(p.Id)).ToList();

            foreach (var saleDetailPost in salePost.SaleDetails)
            {
                var product = products.Single(p => p.Id == saleDetailPost.Product);
                var saleDetail = new SaleDetail();

                saleDetail.Quantity = saleDetailPost.Quantity;
                saleDetail.Product = product;
                saleDetail.PurchasePrice = product.RetailPrice * saleDetailPost.Quantity;
                saleDetail.Tax = product.Tax.TaxRate * saleDetailPost.Quantity;

                sale.SaleDetails.Add(saleDetail);
            }

            sale.Subtotal = sale.SaleDetails.Sum(x => x.PurchasePrice);
            sale.Tax = sale.SaleDetails.Sum(x => x.Tax);
            sale.Total = sale.Subtotal + sale.Tax;
            sale.User = context.Users.Find(userId);

            context.Sales.Add(sale);
            context.SaveChanges();
        }


        public List<Sale> GetSaleReport()
        {
            var context = new TCRMContext("TCRMData");
            return context.Sales.Include("SaleDetails").ToList();
        }
    }
}
