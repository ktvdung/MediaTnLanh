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
    public class SachsController : BaseController
    {
        public static SachsController Context = new SachsController();

        public IEnumerable<SachModel> All()
        {
            var Sachs = dbMediaTinLanh.Sachs.All();
            return Mapper.Map<IEnumerable<Sach>, IEnumerable<SachModel>>(Sachs);
        }

        public IEnumerable<SachModel> Query(string filter, params object[] paramaters)
        {
            var Sachs = dbMediaTinLanh.Sachs.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<Sach>, IEnumerable<SachModel>>(Sachs);
        }

        public SachModel Single(int? Id)
        {
            var Sach = dbMediaTinLanh.Sachs.Single(Id);
            return Mapper.Map<Sach, SachModel>(Sach);
        }

        public int? Insert(SachModel SachModel)
        {
            var Sach = Mapper.Map<SachModel, Sach>(SachModel);
            return dbMediaTinLanh.Sachs.Insert(Sach);
        }

        public int? Update(int? Id, SachModel SachModel)
        {
            var SachExists = dbMediaTinLanh.Sachs.Single(Id);
            if (SachExists != null)
            {
                SachModel.Id = SachExists.Id;
                dbMediaTinLanh.Sachs.Update(Mapper.Map<SachModel, Sach>(SachModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var SachExists = dbMediaTinLanh.Sachs.Single(Id);
            if (SachExists != null)
            {
                dbMediaTinLanh.Sachs.Delete(SachExists);
                return 1;
            }
            return 0;
        }
    }
}
