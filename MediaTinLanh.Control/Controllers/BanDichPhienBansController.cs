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
    public class BanDichPhienBansController : BaseController
    {
        public static BanDichPhienBansController Context = new BanDichPhienBansController();

        public IEnumerable<BanDichPhienBanModel> All()
        {
            var BanDichPhienBans = dbMediaTinLanh.BanDichPhienBans.All();
            return Mapper.Map<IEnumerable<BanDichPhienBan>, IEnumerable<BanDichPhienBanModel>>(BanDichPhienBans);
        }

        public IEnumerable<BanDichPhienBanModel> Query(string filter, params object[] paramaters)
        {
            var BanDichPhienBans = dbMediaTinLanh.BanDichPhienBans.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<BanDichPhienBan>, IEnumerable<BanDichPhienBanModel>>(BanDichPhienBans);
        }

        public BanDichPhienBanModel Single(int? Id)
        {
            var BanDichPhienBan = dbMediaTinLanh.BanDichPhienBans.Single(Id);
            return Mapper.Map<BanDichPhienBan, BanDichPhienBanModel>(BanDichPhienBan);
        }

        public int? Insert(BanDichPhienBanModel BanDichPhienBanModel)
        {
            var BanDichPhienBan = Mapper.Map<BanDichPhienBanModel, BanDichPhienBan>(BanDichPhienBanModel);
            return dbMediaTinLanh.BanDichPhienBans.Insert(BanDichPhienBan);
        }

        public int? Update(int? Id, BanDichPhienBanModel BanDichPhienBanModel)
        {
            var BanDichPhienBanExists = dbMediaTinLanh.BanDichPhienBans.Single(Id);
            if (BanDichPhienBanExists != null)
            {
                BanDichPhienBanModel.Id = BanDichPhienBanExists.Id;
                dbMediaTinLanh.BanDichPhienBans.Update(Mapper.Map<BanDichPhienBanModel, BanDichPhienBan>(BanDichPhienBanModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var BanDichPhienBanExists = dbMediaTinLanh.BanDichPhienBans.Single(Id);
            if (BanDichPhienBanExists != null)
            {
                dbMediaTinLanh.BanDichPhienBans.Delete(BanDichPhienBanExists);
                return 1;
            }
            return 0;
        }
    }
}
