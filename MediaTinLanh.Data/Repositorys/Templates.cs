using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class Templates : Repository<Template>
    {
        public Templates(MediaTinLanhContext context) : base(context) { }
    }
}
