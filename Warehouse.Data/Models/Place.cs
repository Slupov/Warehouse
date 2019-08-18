using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse.Data.Models
{
    /// <summary>
    /// A model describing a single place (store, shop, ) of a company.
    /// Part (or all) of the products reside in a single place.
    /// </summary>
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
