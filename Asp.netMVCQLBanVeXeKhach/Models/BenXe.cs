using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class BenXe
    {
        public BenXe()
        {
            TuyenXes = new HashSet<TuyenXe>();
        }
        [DisplayName("Mã bến")]
        public int MaBenXe { get; set; }
        [DisplayName("Tên bến")]

        public string? TenBenXe { get; set; }
        [DisplayName("Địa chỉ")]

        public string? DiaChi { get; set; }
        [DisplayName("SĐT")]

        public string? Sdt { get; set; }

        public virtual ICollection<TuyenXe> TuyenXes { get; set; }
    }
}
