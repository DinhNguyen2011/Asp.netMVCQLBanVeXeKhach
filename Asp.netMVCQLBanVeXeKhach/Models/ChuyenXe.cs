    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    namespace Asp.netMVCQLBanVeXeKhach.Models
    {
        public partial class ChuyenXe
        {
            public ChuyenXe()
            {
                VeXes = new HashSet<VeXe>();
            }
            [DisplayName("Mã chuyến xe")]

            public int MaChuyen { get; set; }
            [DisplayName("Mã tuyến")]

            public int? MaTuyen { get; set; }
            [DisplayName("Ngày khởi hành")]

            public DateTime? ThoiDiemKhoiHanh { get; set; }
            [DisplayName("Ngày đến")]

            public DateTime? ThoiDiemDenDuKien { get; set; }
            [DisplayName("Giá vé")]

            public decimal? GiaVe { get; set; }
            [DisplayName("Biển số xe")]

            public string? BienSoXe { get; set; }
            [DisplayName("Tên chuyến xe")]

            public string? TenChuyenXe { get; set; }

            public virtual Xe? BienSoXeNavigation { get; set; }
            public virtual TuyenXe? MaTuyenNavigation { get; set; }
            public virtual ICollection<VeXe> VeXes { get; set; }
        }
    }
