using System.ComponentModel.DataAnnotations;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        public int Id { get; set; }

        public virtual Company Company { get; set; }
        public int CompanyId { get; set; }

        [Display(Name = "Име", Prompt = "Име")]
        public string Name { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentPrint PaymentPrint { get; set; }
    }
}
