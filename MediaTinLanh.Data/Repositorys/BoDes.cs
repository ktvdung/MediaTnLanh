﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class BoDes : Repository<BoDe>
    {
        public BoDes(MediaTinLanhContext context) : base(context) { }
    }
}
