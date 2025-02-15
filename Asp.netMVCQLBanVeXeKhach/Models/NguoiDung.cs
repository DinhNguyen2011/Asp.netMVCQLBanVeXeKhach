using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Asp.netMVCQLBanVeXeKhach.Models
{
    public partial class NguoiDung
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Email")]

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; } = null!;
        [DisplayName("SĐT")]

        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
        public string? Sdt { get; set; }

        [DisplayName("Họ và tên")]

        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        public string? HoTen { get; set; }

        [DisplayName("Mật khẩu")]

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        public string MatKhau { get; set; } = null!;
        [DisplayName("Ngày sinh")]

        public DateTime? NgaySinh { get; set; }
        [DisplayName("Địa chỉ")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự.")]
        public string? DiaChi { get; set; }
        [DisplayName("Mã Quyền")]

        public int? MaQuyen { get; set; }
        [DisplayName("Hình ảnh")]
        public string? HinhAnh { get; set; }

        public virtual PhanQuyen? MaQuyenNavigation { get; set; }
    }
}
