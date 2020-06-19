using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class ThanhCas : Repository<ThanhCa>
    {
        public ThanhCas(MediaTinLanhContext context) : base(context) { }
    }
}
