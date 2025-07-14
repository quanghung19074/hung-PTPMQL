using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MvcMovie.Models{

    public class Daily
    { 
        [Key]
        public string? MaDaiLy { get; set; }
        public string? TenDaiLy { get; set; }
        public string? Diachi { get; set; }
        public string? NguoiDaiDien { get; set; }
        public string? DienThoai { get; set; }
        public string? MaHTPP { get; set; }
   }
}