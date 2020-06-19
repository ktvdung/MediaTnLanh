using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class BanDichPhienBans : Repository<BanDichPhienBan>
    {
        public BanDichPhienBans(MediaTinLanhContext context) : base(context) { }
    }
}
