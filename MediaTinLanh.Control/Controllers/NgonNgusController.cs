using AutoMapper;
using MediaTinLanh.Data;
using MediaTinLanh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbMediaTinLanh = MediaTinLanh.Data.MediaTinLanhDB;

namespace MediaTinLanh.Control
{
    public class NgonNgusController : BaseController
    {
        public static NgonNgusController Context = new NgonNgusController();

        public IEnumerable<NgonNguModel> All()
        {
            return Mapper.Map<IEnumerable<NgonNgu>, IEnumerable<NgonNguModel>>(dbMediaTinLanh.NgonNgus.All());
        }
    }
}
