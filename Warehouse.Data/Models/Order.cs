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

        public string Name { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public PaymentPrint PaymentPrint { get; set; }
    }
}
