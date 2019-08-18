using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.Data.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
