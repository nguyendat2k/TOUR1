﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOUR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TaiKhoan
    {
        public string IdUser { get; set; }

        [DisplayName("Tên Tài Khoản Của Bạn")]
        public string UserName { get; set; }

        [DisplayName("Mật Khẩu Của Bạn")]
        [DataType(DataType.Password)]
        public string PassUser { get; set; }

        [NotMapped]
        [Compare("PassUser")]
        [DisplayName("Vui Lòng Nhập Lại Mật Khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
    }
}
