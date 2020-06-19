using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Model
{
    public class MediaModel
    {
        public int? Id { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string Link { get; set; }
        public int? ChuDeId { get; set; }
        public int? Loai { get; set; }
    }
}
