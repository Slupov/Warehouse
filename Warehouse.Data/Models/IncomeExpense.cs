using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class IncomeExpense
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        public IncomeExpenseType IncomeExpenseType { get; set; }
    }
}
