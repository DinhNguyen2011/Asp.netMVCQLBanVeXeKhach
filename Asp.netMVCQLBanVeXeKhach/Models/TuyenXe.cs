using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class TuyenXe
    {
        public TuyenXe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
        }
        [DisplayName("Mã tuyến")]
        public int MaTuyen { get; set; }
        [DisplayName("Điểm đi")]

        public string DiemDi { get; set; } = null!;
        [DisplayName("Điểm đi")]
        public string DiemDen { get; set; } = null!;
        [DisplayName("Số ngày chạy trong tuần")]
        public int? SoNgayChayTrongTuan { get; set; }
        [DisplayName("Giá vé")]
        public decimal? GiaHienHanh { get; set; }
        [DisplayName("Quảng đường")]
        public int? QuangDuong { get; set; }
        [DisplayName("Bến xe đi")]

        public int? MaBenXe { get; set; }
        [DisplayName("Bến xe đến")]
        public int? MaBenXeDen { get; set; }

        public virtual BenXeDen? MaBenXeDenNavigation { get; set; }
        public virtual BenXe? MaBenXeNavigation { get; set; }
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }


        // Thuộc tính không ánh xạ
        //[DisplayName("Tên tuyến")]
        //public string TenTuyen
        //{
        //    get => $"{DiemDi} - {DiemDen}";
        //}
    }
}
