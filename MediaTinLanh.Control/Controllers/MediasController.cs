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
    public class MediasController : BaseController
    {
        public static MediasController Context = new MediasController();

        public IEnumerable<MediaModel> All()
        {
            var Medias = dbMediaTinLanh.Medias.All();
            return Mapper.Map<IEnumerable<Media>, IEnumerable<MediaModel>>(Medias);
        }

        public IEnumerable<MediaModel> Query(string filter, params object[] paramaters)
        {
            var Medias = dbMediaTinLanh.Medias.All(where: filter, parms: paramaters);
            return Mapper.Map<IEnumerable<Media>, IEnumerable<MediaModel>>(Medias);
        }

        public MediaModel Single(int? Id)
        {
            var Media = dbMediaTinLanh.Medias.Single(Id);
            return Mapper.Map<Media, MediaModel>(Media);
        }

        public int? Insert(MediaModel MediaModel)
        {
            var Media = Mapper.Map<MediaModel, Media>(MediaModel);
            return dbMediaTinLanh.Medias.Insert(Media);
        }

        public int? Update(int? Id, MediaModel MediaModel)
        {
            var MediaExists = dbMediaTinLanh.Medias.Single(Id);
            if (MediaExists != null)
            {
                MediaModel.Id = MediaExists.Id;
                dbMediaTinLanh.Medias.Update(Mapper.Map<MediaModel, Media>(MediaModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var MediaExists = dbMediaTinLanh.Medias.Single(Id);
            if (MediaExists != null)
            {
                dbMediaTinLanh.Medias.Delete(MediaExists);
                return 1;
            }
            return 0;
        }
    }
}
