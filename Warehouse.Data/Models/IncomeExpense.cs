using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class IncomeExpense
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public IncomeExpenseType IncomeExpenseType { get; set; }
    }
}
