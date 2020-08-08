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
    public class CauHoisController : BaseController
    {
        public IEnumerable<CauHoiModel> All()
        {
            var CauHois = dbMediaTinLanh.CauHois.All();
            return Mapper.Map<IEnumerable<CauHoi>, IEnumerable<CauHoiModel>>(CauHois);
        }

        public IEnumerable<CauHoiModel> Query(string filter, params object[] paramaters)
        {
            var CauHois = dbMediaTinLanh.CauHois.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<CauHoi>, IEnumerable<CauHoiModel>>(CauHois);
        }

        public CauHoiModel Single(int? Id)
        {
            var CauHoi = dbMediaTinLanh.CauHois.Single(Id);
            return Mapper.Map<CauHoi, CauHoiModel>(CauHoi);
        }

        public int? Insert(CauHoiModel CauHoiModel)
        {
            var CauHoi = Mapper.Map<CauHoiModel, CauHoi>(CauHoiModel);
            return dbMediaTinLanh.CauHois.Insert(CauHoi);
        }

        public int? Update(int? Id, CauHoiModel CauHoiModel)
        {
            var CauHoiExists = dbMediaTinLanh.CauHois.Single(Id);
            if (CauHoiExists != null)
            {
                CauHoiModel.Id = CauHoiExists.Id;
                dbMediaTinLanh.CauHois.Update(Mapper.Map<CauHoiModel, CauHoi>(CauHoiModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var CauHoiExists = dbMediaTinLanh.CauHois.Single(Id);
            if (CauHoiExists != null)
            {
                dbMediaTinLanh.CauHois.Delete(CauHoiExists);
                return 1;
            }
            return 0;
        }
    }
}
