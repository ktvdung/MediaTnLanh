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
    public class BanDichSachsController : BaseController
    {
        public static BanDichSachsController Context = new BanDichSachsController();

        public IEnumerable<BanDichSachModel> All()
        {
            var BanDichSachs = dbMediaTinLanh.BanDichSachs.All();
            return Mapper.Map<IEnumerable<BanDichSach>, IEnumerable<BanDichSachModel>>(BanDichSachs);
        }

        public IEnumerable<BanDichSachModel> Query(string filter, params object[] paramaters)
        {
            var BanDichSachs = dbMediaTinLanh.BanDichSachs.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<BanDichSach>, IEnumerable<BanDichSachModel>>(BanDichSachs);
        }

        public BanDichSachModel Single(int? Id)
        {
            var BanDichSach = dbMediaTinLanh.BanDichSachs.Single(Id);
            return Mapper.Map<BanDichSach, BanDichSachModel>(BanDichSach);
        }

        public int? Insert(BanDichSachModel BanDichSachModel)
        {
            var BanDichSach = Mapper.Map<BanDichSachModel, BanDichSach>(BanDichSachModel);
            return dbMediaTinLanh.BanDichSachs.Insert(BanDichSach);
        }

        public int? Update(int? Id, BanDichSachModel BanDichSachModel)
        {
            var BanDichSachExists = dbMediaTinLanh.BanDichSachs.Single(Id);
            if (BanDichSachExists != null)
            {
                BanDichSachModel.Id = BanDichSachExists.Id;
                dbMediaTinLanh.BanDichSachs.Update(Mapper.Map<BanDichSachModel, BanDichSach>(BanDichSachModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var BanDichSachExists = dbMediaTinLanh.BanDichSachs.Single(Id);
            if (BanDichSachExists != null)
            {
                dbMediaTinLanh.BanDichSachs.Delete(BanDichSachExists);
                return 1;
            }
            return 0;
        }
    }
}
