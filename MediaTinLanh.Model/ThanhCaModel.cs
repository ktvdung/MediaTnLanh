using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Model
{
    public class ThanhCaModel
    {
        public int? Id { get; set; }
        public int? STT { get; set; }
        public string Ten { get; set; }
        public int? SoCau { get; set; }
        public int? Loai { get; set; }
        public string DiepKhuc { get; set; }
    }
}
