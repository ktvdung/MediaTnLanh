﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class MediaThanhCas : Repository<MediaThanhCa>
    {
        public MediaThanhCas(MediaTinLanhContext context) : base(context) { }
    }
}
