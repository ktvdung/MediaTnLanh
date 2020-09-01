using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbMediaTinLanh = MediaTinLanh.Data.MediaTinLanhContext;

namespace MediaTinLanh.Data
{
    public partial class ThanhCas : Repository<ThanhCa>
    {
        public override IEnumerable<ThanhCa> All(string where = null, string orderBy = null, int top = 0, params object[] parms)
        {

            var thanhCas = base.All(where, orderBy, top, parms).ToList();
            if (thanhCas.Count() != 0)
            {
                for (int i = 0; i <= thanhCas.Count() - 1; i++)
                {
                    var tc = dbMediaTinLanh.ThanhCas.Single(thanhCas[i].Id);
                    thanhCas[i].LoaiThanhCa = tc.LoaiThanhCa;
                    thanhCas[i].DanhSachLoiBaiHat = tc.DanhSachLoiBaiHat;
                    thanhCas[i].DanhSachMedia = tc.DanhSachMedia;
                }
            }

            return thanhCas;
        }
        public override ThanhCa Single(int? id)
        {

            var thanhCa = base.Single(id);
            if (thanhCa != null)
            {
                thanhCa.LoaiThanhCa = dbMediaTinLanh.LoaiThanhCas.All(where: "Id = @0", parms: new object[] { thanhCa.Loai }).FirstOrDefault();
                thanhCa.DanhSachLoiBaiHat = dbMediaTinLanh.LoiBaiHats.All(where: "ThanhCaId = @0", parms: new object[] { thanhCa.Id }).ToList();
                var medias = dbMediaTinLanh.Query("Select md.Id, md.Ten, md.MoTa, md.Link, md.LocalLink, md.ChuDeId, md.Loai, md.LuotXem, md.LuotTai, md.TrangThai from Medias md join MediaThanhCas mdt on md.Id = mdt.MediaId where mdt.ThanhCaId = @0", parms: new object[] { thanhCa.Id }).ToList();
                List<Media> listMedia = new List<Media>();
                if(medias.Count() != 0)
                {
                    foreach (var item in medias)
                    {
                        var temp = new Media()
                        {
                            Id = (int)item.Id,
                            Ten = item.Ten,
                            MoTa = item.MoTa,
                            Link = item.Link,
                            LocalLink = item.LocalLink,
                            ChuDeId = (int)item.ChuDeId,
                            Loai = (int)item.Loai,
                            LuotXem = item.LuotXem != null ? (int)item.LuotXem : 0,
                            LuotTai = item.LuotTai != null ? (int)item.LuotTai : 0,
                            TrangThai = item.TrangThai != null ? (int)item.TrangThai : 0
                        };

                        listMedia.Add(temp);
                    }
                }
                else
                {
                    var temp = new Media();

                    listMedia.Add(temp);
                }

                thanhCa.DanhSachMedia = listMedia;
            }

            return thanhCa;
        }
    }
}
