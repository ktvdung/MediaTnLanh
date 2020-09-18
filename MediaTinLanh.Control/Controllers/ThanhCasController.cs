using AutoMapper;
using ChoETL;
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
    public class ThanhCasController : BaseController
    {
        public IEnumerable<ThanhCaModel> All()
        {
            var thanhCas = dbMediaTinLanh.ThanhCas.All();
            return Mapper.Map<IEnumerable<ThanhCa>, IEnumerable<ThanhCaModel>>(thanhCas);
        }

        public IEnumerable<ThanhCaModel> Query(string filter, params object[] paramaters)
        {
            var thanhCas = dbMediaTinLanh.ThanhCas.All(where: filter, parms: paramaters).ToList();
            for(int i = 0; i < thanhCas.Count(); i++)
            {
                var thanhCa = thanhCas[i];
                if(thanhCa.DanhSachMedia.Count() != 0)
                {
                    for (int j = 0; j < thanhCa.DanhSachMedia.Count(); j++)
                    {
                        var media = thanhCa.DanhSachMedia[j];
                        var mediaLocalLink = media.LocalLink;
                        if (!mediaLocalLink.IsNullOrEmpty())
                        {
                            if (!Control_Files.CheckExit(mediaLocalLink))
                            {
                                thanhCa.TrangThai = 0;
                                dbMediaTinLanh.ThanhCas.Update(thanhCa);
                            }
                        }
                    }
                }
                
            }
            return Mapper.Map<IEnumerable<ThanhCa>, IEnumerable<ThanhCaModel>>(thanhCas);
        }

        public ThanhCaModel Single(int? Id)
        {
            var thanhCa = dbMediaTinLanh.ThanhCas.Single(Id);
            return Mapper.Map<ThanhCa, ThanhCaModel>(thanhCa);
        }

        public int? Insert(ThanhCaModel thanhCaModel)
        {
            var thanhCa = Mapper.Map<ThanhCaModel, ThanhCa>(thanhCaModel);
            return dbMediaTinLanh.ThanhCas.Insert(thanhCa);
        }

        public int? Update(int? Id, ThanhCaModel thanhCaModel)
        {
            var thanhCaExists = dbMediaTinLanh.ThanhCas.Single(Id);
            if (thanhCaExists != null)
            {
                thanhCaModel.Id = thanhCaExists.Id;
                dbMediaTinLanh.ThanhCas.Update(Mapper.Map<ThanhCaModel, ThanhCa>(thanhCaModel));

                return 1;
            }
            return 0;
        }

        public int? Delete(int? Id)
        {
            var thanhCaExists = dbMediaTinLanh.ThanhCas.Single(Id);
            if (thanhCaExists != null)
            {
                dbMediaTinLanh.ThanhCas.Delete(thanhCaExists);
                return 1;
            }
            return 0;
        }
    }
}
