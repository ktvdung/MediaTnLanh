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
    public class BanDichCausController : BaseController
    {
        public IEnumerable<BanDichCauModel> All()
        {
            var BanDichCaus = dbMediaTinLanh.BanDichCaus.All();
            return Mapper.Map<IEnumerable<BanDichCau>, IEnumerable<BanDichCauModel>>(BanDichCaus);
        }

        public IEnumerable<BanDichCauModel> Query(string filter, params object[] paramaters)
        {
            var BanDichCaus = dbMediaTinLanh.BanDichCaus.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<BanDichCau>, IEnumerable<BanDichCauModel>>(BanDichCaus);
        }

        public BanDichCauModel Single(int? Id)
        {
            var BanDichCau = dbMediaTinLanh.BanDichCaus.Single(Id);
            return Mapper.Map<BanDichCau, BanDichCauModel>(BanDichCau);
        }

        public int? Insert(BanDichCauModel BanDichCauModel)
        {
            var BanDichCau = Mapper.Map<BanDichCauModel, BanDichCau>(BanDichCauModel);
            return dbMediaTinLanh.BanDichCaus.Insert(BanDichCau);
        }

        public int? Update(int? Id, BanDichCauModel BanDichCauModel)
        {
            var BanDichCauExists = dbMediaTinLanh.BanDichCaus.Single(Id);
            if (BanDichCauExists != null)
            {
                BanDichCauModel.Id = BanDichCauExists.Id;
                dbMediaTinLanh.BanDichCaus.Update(Mapper.Map<BanDichCauModel, BanDichCau>(BanDichCauModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var BanDichCauExists = dbMediaTinLanh.BanDichCaus.Single(Id);
            if (BanDichCauExists != null)
            {
                dbMediaTinLanh.BanDichCaus.Delete(BanDichCauExists);
                return 1;
            }
            return 0;
        }
    }
}
