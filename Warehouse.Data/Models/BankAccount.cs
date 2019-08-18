using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class BankAccount
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "BIC", Prompt = "BIC")]
        public string BIC { get; set; }

        [Display(Name = "IBAN", Prompt = "IBAN")]
        public string IBAN { get; set; }

        public Currency Currency { get; set; }
    }   
}
