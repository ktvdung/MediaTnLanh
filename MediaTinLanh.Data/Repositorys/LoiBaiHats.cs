﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class LoiBaiHats : Repository<LoiBaiHat>
    {
        public LoiBaiHats(MediaTinLanhContext context) : base(context) { }
    }
}
