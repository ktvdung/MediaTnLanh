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
    public class BoDesController : BaseController
    {
        public IEnumerable<BoDeModel> All()
        {
            var BoDes = dbMediaTinLanh.BoDes.All();
            return Mapper.Map<IEnumerable<BoDe>, IEnumerable<BoDeModel>>(BoDes);
        }

        public IEnumerable<BoDeModel> Query(string filter, params object[] paramaters)
        {
            var BoDes = dbMediaTinLanh.BoDes.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<BoDe>, IEnumerable<BoDeModel>>(BoDes);
        }

        public BoDeModel Single(int? Id)
        {
            var BoDe = dbMediaTinLanh.BoDes.Single(Id);
            return Mapper.Map<BoDe, BoDeModel>(BoDe);
        }

        public int? Insert(BoDeModel BoDeModel)
        {
            var BoDe = Mapper.Map<BoDeModel, BoDe>(BoDeModel);
            return dbMediaTinLanh.BoDes.Insert(BoDe);
        }

        public int? Update(int? Id, BoDeModel BoDeModel)
        {
            var BoDeExists = dbMediaTinLanh.BoDes.Single(Id);
            if (BoDeExists != null)
            {
                BoDeModel.Id = BoDeExists.Id;
                dbMediaTinLanh.BoDes.Update(Mapper.Map<BoDeModel, BoDe>(BoDeModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var BoDeExists = dbMediaTinLanh.BoDes.Single(Id);
            if (BoDeExists != null)
            {
                dbMediaTinLanh.BoDes.Delete(BoDeExists);
                return 1;
            }
            return 0;
        }
    }
}
