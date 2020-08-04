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
    public class CauDosController : BaseController
    {
        public static CauDosController Context = new CauDosController();

        public IEnumerable<CauDoModel> All()
        {
            var CauDos = dbMediaTinLanh.CauDos.All();
            return Mapper.Map<IEnumerable<CauDo>, IEnumerable<CauDoModel>>(CauDos);
        }

        public IEnumerable<CauDoModel> Query(string filter, params object[] paramaters)
        {
            var CauDos = dbMediaTinLanh.CauDos.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<CauDo>, IEnumerable<CauDoModel>>(CauDos);
        }

        public CauDoModel Single(int? Id)
        {
            var CauDo = dbMediaTinLanh.CauDos.Single(Id);
            return Mapper.Map<CauDo, CauDoModel>(CauDo);
        }

        public int? Insert(CauDoModel CauDoModel)
        {
            var CauDo = Mapper.Map<CauDoModel, CauDo>(CauDoModel);
            return dbMediaTinLanh.CauDos.Insert(CauDo);
        }

        public int? Update(int? Id, CauDoModel CauDoModel)
        {
            var CauDoExists = dbMediaTinLanh.CauDos.Single(Id);
            if (CauDoExists != null)
            {
                CauDoModel.Id = CauDoExists.Id;
                dbMediaTinLanh.CauDos.Update(Mapper.Map<CauDoModel, CauDo>(CauDoModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var CauDoExists = dbMediaTinLanh.CauDos.Single(Id);
            if (CauDoExists != null)
            {
                dbMediaTinLanh.CauDos.Delete(CauDoExists);
                return 1;
            }
            return 0;
        }
    }
}
