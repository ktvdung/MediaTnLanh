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
    public class DapAnsController : BaseController
    {
        public static DapAnsController Context = new DapAnsController();

        public IEnumerable<DapAnModel> All()
        {
            var DapAns = dbMediaTinLanh.DapAns.All();
            return Mapper.Map<IEnumerable<DapAn>, IEnumerable<DapAnModel>>(DapAns);
        }

        public IEnumerable<DapAnModel> Query(string filter, params object[] paramaters)
        {
            var DapAns = dbMediaTinLanh.DapAns.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<DapAn>, IEnumerable<DapAnModel>>(DapAns);
        }

        public DapAnModel Single(int? Id)
        {
            var DapAn = dbMediaTinLanh.DapAns.Single(Id);
            return Mapper.Map<DapAn, DapAnModel>(DapAn);
        }

        public int? Insert(DapAnModel DapAnModel)
        {
            var DapAn = Mapper.Map<DapAnModel, DapAn>(DapAnModel);
            return dbMediaTinLanh.DapAns.Insert(DapAn);
        }

        public int? Update(int? Id, DapAnModel DapAnModel)
        {
            var DapAnExists = dbMediaTinLanh.DapAns.Single(Id);
            if (DapAnExists != null)
            {
                DapAnModel.Id = DapAnExists.Id;
                dbMediaTinLanh.DapAns.Update(Mapper.Map<DapAnModel, DapAn>(DapAnModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var DapAnExists = dbMediaTinLanh.DapAns.Single(Id);
            if (DapAnExists != null)
            {
                dbMediaTinLanh.DapAns.Delete(DapAnExists);
                return 1;
            }
            return 0;
        }
    }
}
