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
    public class CauKinhThanhsController : BaseController
    {
        public IEnumerable<CauKinhThanhModel> All()
        {
            var CauKinhThanhs = dbMediaTinLanh.CauKinhThanhs.All();
            return Mapper.Map<IEnumerable<CauKinhThanh>, IEnumerable<CauKinhThanhModel>>(CauKinhThanhs);
        }

        public IEnumerable<CauKinhThanhModel> Query(string filter, params object[] paramaters)
        {
            var CauKinhThanhs = dbMediaTinLanh.CauKinhThanhs.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<CauKinhThanh>, IEnumerable<CauKinhThanhModel>>(CauKinhThanhs);
        }

        public CauKinhThanhModel Single(int? Id)
        {
            var CauKinhThanh = dbMediaTinLanh.CauKinhThanhs.Single(Id);
            return Mapper.Map<CauKinhThanh, CauKinhThanhModel>(CauKinhThanh);
        }

        public int? Insert(CauKinhThanhModel CauKinhThanhModel)
        {
            var CauKinhThanh = Mapper.Map<CauKinhThanhModel, CauKinhThanh>(CauKinhThanhModel);
            return dbMediaTinLanh.CauKinhThanhs.Insert(CauKinhThanh);
        }

        public int? Update(int? Id, CauKinhThanhModel CauKinhThanhModel)
        {
            var CauKinhThanhExists = dbMediaTinLanh.CauKinhThanhs.Single(Id);
            if (CauKinhThanhExists != null)
            {
                CauKinhThanhModel.Id = CauKinhThanhExists.Id;
                dbMediaTinLanh.CauKinhThanhs.Update(Mapper.Map<CauKinhThanhModel, CauKinhThanh>(CauKinhThanhModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var CauKinhThanhExists = dbMediaTinLanh.CauKinhThanhs.Single(Id);
            if (CauKinhThanhExists != null)
            {
                dbMediaTinLanh.CauKinhThanhs.Delete(CauKinhThanhExists);
                return 1;
            }
            return 0;
        }
    }
}
