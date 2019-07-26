using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warehouse.Data.Enums;

namespace Warehouse.Web.Areas.Payments.Models
{
    public class PaymentCreateViewModel
    {
        public string Name { get; set; }

        public SelectList PaymentMethods { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public SelectList PaymentPrints { get; set; }
        public PaymentPrint PaymentPrint { get; set; }
    }
}
