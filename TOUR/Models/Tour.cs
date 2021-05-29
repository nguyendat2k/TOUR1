//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOUR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tour
    {
        public Tour()
        {
            this.Chuyens = new HashSet<Chuyen>();
            this.DungChans = new HashSet<DungChan>();
            this.PhieuDoans = new HashSet<PhieuDoan>();
            this.KhachSans = new HashSet<KhachSan>();
            this.PhuongTiens = new HashSet<PhuongTien>();
        }
    
        public string MaTour { get; set; }
        public string TenTour { get; set; }
        public string MoTa { get; set; }
        public Nullable<byte> SoNgay { get; set; }
        public Nullable<double> Gia { get; set; }
        public Nullable<System.DateTime> NgayKhoiHanh { get; set; }
        public string Anhbia { get; set; }
    
        public virtual ICollection<Chuyen> Chuyens { get; set; }
        public virtual ICollection<DungChan> DungChans { get; set; }
        public virtual ICollection<PhieuDoan> PhieuDoans { get; set; }
        public virtual ICollection<KhachSan> KhachSans { get; set; }
        public virtual ICollection<PhuongTien> PhuongTiens { get; set; }
    }
}
