using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Model
{
    public class BasketVoucher
    {
        public int BasketVoucherId { get; set; }
        public int VoucherId { get; set; }
        public Guid BasketId { get; set; }
        public string VoucherCode { get; set; }

        public string VoucherType { get; set; }
        public decimal Value { get; set; }
        public string VoucherDescription { get; set; }
        public int AppliedProductId { get; set; }
    }
}
