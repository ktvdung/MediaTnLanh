﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class MediaTypes : Repository<MediaType>
    {
        public MediaTypes(MediaTinLanhContext context) : base(context) { }
    }
}
