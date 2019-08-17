using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Data.Enums;

namespace Warehouse.Web.Areas.Orders.Models
{
    public class OrderCreateViewModel
    {
        public string Name { get; set; }

        public SelectList PaymentMethods { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public SelectList PaymentPrints { get; set; }
        public PaymentPrint PaymentPrint { get; set; }
    }
}
