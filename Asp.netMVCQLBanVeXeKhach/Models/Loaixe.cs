using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class Loaixe
    {
        public Loaixe()
        {
            Xes = new HashSet<Xe>();
        }
        [DisplayName("ID loại xe")]

        public int IdLoai { get; set; }
        [DisplayName("Tên loại")]

        public string? Tenloai { get; set; }
        [DisplayName("Số ghế")]

        public int Soghe { get; set; }

        public virtual ICollection<Xe> Xes { get; set; }
    }
}
