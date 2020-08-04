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

        public IEnumerable<NgonNguModel> Query(string filter, params object[] paramaters)
        {
            var NgonNgus = dbMediaTinLanh.NgonNgus.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<NgonNgu>, IEnumerable<NgonNguModel>>(NgonNgus);
        }

        public NgonNguModel Single(int? Id)
        {
            var NgonNgu = dbMediaTinLanh.NgonNgus.Single(Id);
            return Mapper.Map<NgonNgu, NgonNguModel>(NgonNgu);
        }

        public int? Insert(NgonNguModel NgonNguModel)
        {
            var NgonNgu = Mapper.Map<NgonNguModel, NgonNgu>(NgonNguModel);
            return dbMediaTinLanh.NgonNgus.Insert(NgonNgu);
        }

        public int? Update(int? Id, NgonNguModel NgonNguModel)
        {
            var NgonNguExists = dbMediaTinLanh.NgonNgus.Single(Id);
            if (NgonNguExists != null)
            {
                NgonNguModel.Id = NgonNguExists.Id;
                dbMediaTinLanh.NgonNgus.Update(Mapper.Map<NgonNguModel, NgonNgu>(NgonNguModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var NgonNguExists = dbMediaTinLanh.NgonNgus.Single(Id);
            if (NgonNguExists != null)
            {
                dbMediaTinLanh.NgonNgus.Delete(NgonNguExists);
                return 1;
            }
            return 0;
        }
    }
}
