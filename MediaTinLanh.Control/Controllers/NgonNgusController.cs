using AutoMapper;
using MediaTinLanh.Data;
using MediaTinLanh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbMediaTinLanh = MediaTinLanh.Data.MediaTinLanhContext;

namespace MediaTinLanh.Control
{
    public class NgonNgusController : BaseController
    {
        public static NgonNgusController Context = new NgonNgusController();

        public IEnumerable<NgonNguModel> All()
        {
            var ngonNgus = dbMediaTinLanh.NgonNgus.All();
            return Mapper.Map<IEnumerable<NgonNgu>, IEnumerable<NgonNguModel>>(ngonNgus);
        }
    }
}
