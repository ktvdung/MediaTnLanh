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
    public class LoiBaiHatsController : BaseController
    {
        public IEnumerable<LoiBaiHatModel> All()
        {
            var loiBaiHats = dbMediaTinLanh.LoiBaiHats.All();
            return Mapper.Map<IEnumerable<LoiBaiHat>, IEnumerable<LoiBaiHatModel>>(loiBaiHats);
        }

        public IEnumerable<LoiBaiHatModel> Query(string filter, params object[] paramaters)
        {
            var loiBaiHats = dbMediaTinLanh.LoiBaiHats.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<LoiBaiHat>, IEnumerable<LoiBaiHatModel>>(loiBaiHats);
        }

        public LoiBaiHatModel Single(int? Id)
        {
            var loiBaiHat = dbMediaTinLanh.LoiBaiHats.Single(Id);
            return Mapper.Map<LoiBaiHat, LoiBaiHatModel>(loiBaiHat);
        }

        public IEnumerable<LoiBaiHatModel> GetDSLoiBaiHatByThanhCa(int? thanhCaId)
        {
            var loiBaiHats = dbMediaTinLanh.LoiBaiHats.All("ThanhCaId = @0", parms: new object[] { thanhCaId });
            return Mapper.Map<IEnumerable<LoiBaiHat>, IEnumerable<LoiBaiHatModel>>(loiBaiHats);
        }

        public int? Insert(LoiBaiHatModel LoiBaiHatModel)
        {
            var LoiBaiHat = Mapper.Map<LoiBaiHatModel, LoiBaiHat>(LoiBaiHatModel);
            return dbMediaTinLanh.LoiBaiHats.Insert(LoiBaiHat);
        }

        public int? Update(int? Id, LoiBaiHatModel LoiBaiHatModel)
        {
            var LoiBaiHatExists = dbMediaTinLanh.LoiBaiHats.Single(Id);
            if (LoiBaiHatExists != null)
            {
                LoiBaiHatModel.Id = LoiBaiHatExists.Id;
                dbMediaTinLanh.LoiBaiHats.Update(Mapper.Map<LoiBaiHatModel, LoiBaiHat>(LoiBaiHatModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var LoiBaiHatExists = dbMediaTinLanh.LoiBaiHats.Single(Id);
            if (LoiBaiHatExists != null)
            {
                dbMediaTinLanh.LoiBaiHats.Delete(LoiBaiHatExists);
                return 1;
            }
            return 0;
        }
    }
}
