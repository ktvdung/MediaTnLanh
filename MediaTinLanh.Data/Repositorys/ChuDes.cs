﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class ChuDes : Repository<ChuDe>
    {
        public ChuDes(MediaTinLanhContext context) : base(context) { }
    }
}
