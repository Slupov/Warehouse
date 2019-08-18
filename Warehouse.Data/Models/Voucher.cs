using System.ComponentModel.DataAnnotations;
using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        [Display(Name = "Тип ваучер", Prompt = "Тип ваучер")]
        public VoucherType VoucherType { get; set; }

        [Display(Name = "Код на ваучер", Prompt = "Код на ваучер")]
        public string UniqueCode { get; set; }
    }
}
