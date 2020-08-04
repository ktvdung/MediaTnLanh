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
    public class MediaTypesController : BaseController
    {
        public static MediaTypesController Context = new MediaTypesController();

        public IEnumerable<MediaTypeModel> All()
        {
            var MediaTypes = dbMediaTinLanh.MediaTypes.All();
            return Mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeModel>>(MediaTypes);
        }

        public IEnumerable<MediaTypeModel> Query(string filter, params object[] paramaters)
        {
            var MediaTypes = dbMediaTinLanh.MediaTypes.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeModel>>(MediaTypes);
        }

        public MediaTypeModel Single(int? Id)
        {
            var MediaType = dbMediaTinLanh.MediaTypes.Single(Id);
            return Mapper.Map<MediaType, MediaTypeModel>(MediaType);
        }

        public int? Insert(MediaTypeModel MediaTypeModel)
        {
            var MediaType = Mapper.Map<MediaTypeModel, MediaType>(MediaTypeModel);
            return dbMediaTinLanh.MediaTypes.Insert(MediaType);
        }

        public int? Update(int? Id, MediaTypeModel MediaTypeModel)
        {
            var MediaTypeExists = dbMediaTinLanh.MediaTypes.Single(Id);
            if (MediaTypeExists != null)
            {
                MediaTypeModel.Id = MediaTypeExists.Id;
                dbMediaTinLanh.MediaTypes.Update(Mapper.Map<MediaTypeModel, MediaType>(MediaTypeModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var MediaTypeExists = dbMediaTinLanh.MediaTypes.Single(Id);
            if (MediaTypeExists != null)
            {
                dbMediaTinLanh.MediaTypes.Delete(MediaTypeExists);
                return 1;
            }
            return 0;
        }
    }
}
