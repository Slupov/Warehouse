using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Заявено на", Prompt = "Заявено на")]
        public DateTime RequestedOn { get; set; }

        [Display(Name = "Платено на", Prompt = "Платено на")]
        public DateTime PayedOn { get; set; }

        [Display(Name = "За месеци", Prompt = "За месеци")]
        public int ForMonths { get; set; }

        [Display(Name = "За потребители", Prompt = "За потребители")]
        public int ForUsers { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        [Display(Name = "Тотална цена", Prompt = "Тотална цена")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Статус на плащането", Prompt = "Статус на плащането")]
        public PaymentStatus PaymentStatus { get; set; }

        //TODO Stoyan Lupov 29 June, 2019 Add voucher
    }
}
