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
    public class MediaThanhCasController : BaseController
    {
        public static MediaThanhCasController Context = new MediaThanhCasController();

        public IEnumerable<MediaThanhCaModel> All()
        {
            var MediaThanhCas = dbMediaTinLanh.MediaThanhCas.All();
            return Mapper.Map<IEnumerable<MediaThanhCa>, IEnumerable<MediaThanhCaModel>>(MediaThanhCas);
        }

        public IEnumerable<MediaThanhCaModel> Query(string filter, params object[] paramaters)
        {
            var MediaThanhCas = dbMediaTinLanh.MediaThanhCas.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<MediaThanhCa>, IEnumerable<MediaThanhCaModel>>(MediaThanhCas);
        }

        public MediaThanhCaModel Single(int? Id)
        {
            var MediaThanhCa = dbMediaTinLanh.MediaThanhCas.Single(Id);
            return Mapper.Map<MediaThanhCa, MediaThanhCaModel>(MediaThanhCa);
        }

        public int? Insert(MediaThanhCaModel MediaThanhCaModel)
        {
            var MediaThanhCa = Mapper.Map<MediaThanhCaModel, MediaThanhCa>(MediaThanhCaModel);
            return dbMediaTinLanh.MediaThanhCas.Insert(MediaThanhCa);
        }

        public int? Update(int? Id, MediaThanhCaModel MediaThanhCaModel)
        {
            var MediaThanhCaExists = dbMediaTinLanh.MediaThanhCas.Single(Id);
            if (MediaThanhCaExists != null)
            {
                MediaThanhCaModel.Id = MediaThanhCaExists.Id;
                dbMediaTinLanh.MediaThanhCas.Update(Mapper.Map<MediaThanhCaModel, MediaThanhCa>(MediaThanhCaModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var MediaThanhCaExists = dbMediaTinLanh.MediaThanhCas.Single(Id);
            if (MediaThanhCaExists != null)
            {
                dbMediaTinLanh.MediaThanhCas.Delete(MediaThanhCaExists);
                return 1;
            }
            return 0;
        }
    }
}
