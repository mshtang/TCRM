﻿using System;

namespace TCRMDataManager.Library.Models
{
    public class SaleDb
    {
        public int Id { get; set; }

        public string CashierId { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        public decimal Subtotal { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }
    }
}
