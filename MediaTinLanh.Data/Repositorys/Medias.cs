﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Data
{
    public partial class Medias : Repository<Media>
    {
        public Medias(MediaTinLanhContext context) : base(context) { }
    }
}
