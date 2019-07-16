using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public  class Subscription
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        public DateTime RequestedOn { get; set; }
        public DateTime PayedOn { get; set; }

        public int ForMonths { get; set; }
        public int ForUsers { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal TotalPrice { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 Add voucher
    }
}
