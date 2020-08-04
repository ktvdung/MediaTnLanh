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
    public class LoaiThanhCasController : BaseController
    {
        public static LoaiThanhCasController Context = new LoaiThanhCasController();

        public IEnumerable<LoaiThanhCaModel> All()
        {
            var LoaiThanhCas = dbMediaTinLanh.LoaiThanhCas.All();
            return Mapper.Map<IEnumerable<LoaiThanhCa>, IEnumerable<LoaiThanhCaModel>>(LoaiThanhCas);
        }

        public IEnumerable<LoaiThanhCaModel> Query(string filter, params object[] paramaters)
        {
            var LoaiThanhCas = dbMediaTinLanh.LoaiThanhCas.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<LoaiThanhCa>, IEnumerable<LoaiThanhCaModel>>(LoaiThanhCas);
        }

        public LoaiThanhCaModel Single(int? Id)
        {
            var LoaiThanhCa = dbMediaTinLanh.LoaiThanhCas.Single(Id);
            return Mapper.Map<LoaiThanhCa, LoaiThanhCaModel>(LoaiThanhCa);
        }

        public int? Insert(LoaiThanhCaModel LoaiThanhCaModel)
        {
            var LoaiThanhCa = Mapper.Map<LoaiThanhCaModel, LoaiThanhCa>(LoaiThanhCaModel);
            return dbMediaTinLanh.LoaiThanhCas.Insert(LoaiThanhCa);
        }

        public int? Update(int? Id, LoaiThanhCaModel LoaiThanhCaModel)
        {
            var LoaiThanhCaExists = dbMediaTinLanh.LoaiThanhCas.Single(Id);
            if (LoaiThanhCaExists != null)
            {
                LoaiThanhCaModel.Id = LoaiThanhCaExists.Id;
                dbMediaTinLanh.LoaiThanhCas.Update(Mapper.Map<LoaiThanhCaModel, LoaiThanhCa>(LoaiThanhCaModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var LoaiThanhCaExists = dbMediaTinLanh.LoaiThanhCas.Single(Id);
            if (LoaiThanhCaExists != null)
            {
                dbMediaTinLanh.LoaiThanhCas.Delete(LoaiThanhCaExists);
                return 1;
            }
            return 0;
        }
    }
}
