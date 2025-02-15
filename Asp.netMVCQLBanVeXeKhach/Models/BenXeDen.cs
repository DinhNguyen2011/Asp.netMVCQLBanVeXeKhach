using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class BenXeDen
    {
        public BenXeDen()
        {
            TuyenXes = new HashSet<TuyenXe>();
        }
        [DisplayName("Mã bến")]

        public int MaBenXeDen { get; set; }
        [DisplayName("Tên bến đến")]

        public string? TenBenXeDen { get; set; }
        [DisplayName("Địa chỉ")]

        public string? DiaChi { get; set; }
        [DisplayName("SĐT")]

        public string? Sdt { get; set; }

        public virtual ICollection<TuyenXe> TuyenXes { get; set; }
    }
}
