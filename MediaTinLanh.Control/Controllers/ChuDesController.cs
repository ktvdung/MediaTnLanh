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
    public class ChuDesController : BaseController
    {
        public IEnumerable<ChuDeModel> All()
        {
            var ChuDes = dbMediaTinLanh.ChuDes.All();
            return Mapper.Map<IEnumerable<ChuDe>, IEnumerable<ChuDeModel>>(ChuDes);
        }

        public IEnumerable<ChuDeModel> Query(string filter, params object[] paramaters)
        {
            var ChuDes = dbMediaTinLanh.ChuDes.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<ChuDe>, IEnumerable<ChuDeModel>>(ChuDes);
        }

        public ChuDeModel Single(int? Id)
        {
            var ChuDe = dbMediaTinLanh.ChuDes.Single(Id);
            return Mapper.Map<ChuDe, ChuDeModel>(ChuDe);
        }

        public int? Insert(ChuDeModel ChuDeModel)
        {
            var ChuDe = Mapper.Map<ChuDeModel, ChuDe>(ChuDeModel);
            return dbMediaTinLanh.ChuDes.Insert(ChuDe);
        }

        public int? Update(int? Id, ChuDeModel ChuDeModel)
        {
            var ChuDeExists = dbMediaTinLanh.ChuDes.Single(Id);
            if (ChuDeExists != null)
            {
                ChuDeModel.Id = ChuDeExists.Id;
                dbMediaTinLanh.ChuDes.Update(Mapper.Map<ChuDeModel, ChuDe>(ChuDeModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var ChuDeExists = dbMediaTinLanh.ChuDes.Single(Id);
            if (ChuDeExists != null)
            {
                dbMediaTinLanh.ChuDes.Delete(ChuDeExists);
                return 1;
            }
            return 0;
        }
    }
}
