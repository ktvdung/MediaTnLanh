using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class ThanhCa : Entity<ThanhCa>
    {
        public LoaiThanhCa LoaiThanhCa { get; set; }
        public List<LoiBaiHat> DanhSachLoiBaiHat { get; set; }
        public List<Media> DanhSachMedia { get; set; }
    }
}
