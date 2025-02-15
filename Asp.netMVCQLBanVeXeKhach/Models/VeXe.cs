using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class VeXe
    {
        [DisplayName("Mã vé")]   
        
        public int MaVe { get; set; }
        [DisplayName("Mã phiếu")]

        public int? MaPhieu { get; set; }
        [DisplayName("Mã chuyến")]

        public int? MaChuyen { get; set; }
        [DisplayName("Mã vé")]

        public string? TenVe { get; set; }
        [DisplayName("Tên vé")]

        public string? TrangThai { get; set; }
        [DisplayName("Trạng thái")]

        public string? GhiChu { get; set; }
        [DisplayName("Tên KH")]

        public string? TenKh { get; set; }
        [DisplayName("Em")]

        public string? Email { get; set; }
        [DisplayName("Mã vé")]

        public int? IdVitri { get; set; }

        public virtual Vitrighe? IdVitriNavigation { get; set; }
        public virtual ChuyenXe? MaChuyenNavigation { get; set; }
        public virtual PhieuDatVe? MaPhieuNavigation { get; set; }
    }
}
