using Warehouse.Data.Enums;

namespace Warehouse.Data.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        public VoucherType VoucherType { get; set; }

        public string UniqueCode { get; set; }
    }
}
