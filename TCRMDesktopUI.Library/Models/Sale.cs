using System.Collections.Generic;

namespace TCRMDesktopUI.Library.Models
{
    public class Sale
    {
        public List<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
