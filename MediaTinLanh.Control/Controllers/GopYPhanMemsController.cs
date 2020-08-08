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
    public class GopYPhanMemsController : BaseController
    {
        public IEnumerable<GopYPhanMemModel> All()
        {
            var GopYPhanMems = dbMediaTinLanh.GopYPhanMems.All();
            return Mapper.Map<IEnumerable<GopYPhanMem>, IEnumerable<GopYPhanMemModel>>(GopYPhanMems);
        }

        public IEnumerable<GopYPhanMemModel> Query(string filter, params object[] paramaters)
        {
            var GopYPhanMems = dbMediaTinLanh.GopYPhanMems.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<GopYPhanMem>, IEnumerable<GopYPhanMemModel>>(GopYPhanMems);
        }

        public GopYPhanMemModel Single(int? Id)
        {
            var GopYPhanMem = dbMediaTinLanh.GopYPhanMems.Single(Id);
            return Mapper.Map<GopYPhanMem, GopYPhanMemModel>(GopYPhanMem);
        }

        public int? Insert(GopYPhanMemModel GopYPhanMemModel)
        {
            var GopYPhanMem = Mapper.Map<GopYPhanMemModel, GopYPhanMem>(GopYPhanMemModel);
            return dbMediaTinLanh.GopYPhanMems.Insert(GopYPhanMem);
        }

        public int? Update(int? Id, GopYPhanMemModel GopYPhanMemModel)
        {
            var GopYPhanMemExists = dbMediaTinLanh.GopYPhanMems.Single(Id);
            if (GopYPhanMemExists != null)
            {
                GopYPhanMemModel.Id = GopYPhanMemExists.Id;
                dbMediaTinLanh.GopYPhanMems.Update(Mapper.Map<GopYPhanMemModel, GopYPhanMem>(GopYPhanMemModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var GopYPhanMemExists = dbMediaTinLanh.GopYPhanMems.Single(Id);
            if (GopYPhanMemExists != null)
            {
                dbMediaTinLanh.GopYPhanMems.Delete(GopYPhanMemExists);
                return 1;
            }
            return 0;
        }
    }
}
