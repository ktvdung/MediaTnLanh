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
    public class PhienBansController : BaseController
    {
        public IEnumerable<PhienBanModel> All()
        {
            var PhienBans = dbMediaTinLanh.PhienBans.All();
            return Mapper.Map<IEnumerable<PhienBan>, IEnumerable<PhienBanModel>>(PhienBans);
        }

        public IEnumerable<PhienBanModel> Query(string filter, params object[] paramaters)
        {
            var PhienBans = dbMediaTinLanh.PhienBans.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<PhienBan>, IEnumerable<PhienBanModel>>(PhienBans);
        }

        public PhienBanModel Single(int? Id)
        {
            var PhienBan = dbMediaTinLanh.PhienBans.Single(Id);
            return Mapper.Map<PhienBan, PhienBanModel>(PhienBan);
        }

        public int? Insert(PhienBanModel PhienBanModel)
        {
            var PhienBan = Mapper.Map<PhienBanModel, PhienBan>(PhienBanModel);
            return dbMediaTinLanh.PhienBans.Insert(PhienBan);
        }

        public int? Update(int? Id, PhienBanModel PhienBanModel)
        {
            var PhienBanExists = dbMediaTinLanh.PhienBans.Single(Id);
            if (PhienBanExists != null)
            {
                PhienBanModel.Id = PhienBanExists.Id;
                dbMediaTinLanh.PhienBans.Update(Mapper.Map<PhienBanModel, PhienBan>(PhienBanModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var PhienBanExists = dbMediaTinLanh.PhienBans.Single(Id);
            if (PhienBanExists != null)
            {
                dbMediaTinLanh.PhienBans.Delete(PhienBanExists);
                return 1;
            }
            return 0;
        }
    }
}
