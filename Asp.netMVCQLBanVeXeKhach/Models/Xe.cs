using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class Xe
    {
        public Xe()
        {
            ChuyenXes = new HashSet<ChuyenXe>();
            Vitrighes = new HashSet<Vitrighe>();
        }
        [DisplayName("Biển số")]
        public string Bienso { get; set; } = null!;
        [DisplayName("id Loại")]

        public int IdLoai { get; set; }
        [DisplayName("Tên Xe")]


        public string? Tenxe { get; set; }
        [DisplayName("Hình ảnh")]

        public string? HinhAnh { get; set; }

        public virtual Loaixe IdLoaiNavigation { get; set; } = null!;
        public virtual ICollection<ChuyenXe> ChuyenXes { get; set; }
        public virtual ICollection<Vitrighe> Vitrighes { get; set; }
    }
}
