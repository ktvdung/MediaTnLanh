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
    public class Factory
    {
        public static LoaiThanhCasController LoaiThanhCaService { get { return new LoaiThanhCasController(); } }
        public static ThanhCasController ThanhCaService { get { return new ThanhCasController(); } }
        public static LoiBaiHatsController LoiBaiHatService { get { return new LoiBaiHatsController(); } }
        public static BanDichCausController BanDichCauKinhThanhService { get { return new BanDichCausController(); } }
        public static BanDichPhienBansController BanDichPhienBanService { get { return new BanDichPhienBansController(); } }
        public static BanDichSachsController BanDichSachService { get { return new BanDichSachsController(); } }
        public static SachsController SachService { get { return new SachsController(); } }
        public static CauKinhThanhsController CauKinhThanhService { get { return new CauKinhThanhsController(); } }
        public static PhienBansController PhienBanService { get { return new PhienBansController(); } }
        public static ChuDesController ChuDeService { get { return new ChuDesController(); } }
        public static BoDesController BoDeService { get { return new BoDesController(); } }
        public static CauDosController CauDoService { get { return new CauDosController(); } }
        public static CauHoisController CauHoiService { get { return new CauHoisController(); } }
        public static MediaTypesController MediaTypeService { get { return new MediaTypesController(); } }
        public static MediasController MediaService { get { return new MediasController(); } }
        public static MediaThanhCasController MediaThanhCaService { get { return new MediaThanhCasController(); } }
        public static TemplatesController TemplateService { get { return new TemplatesController(); } }
        public static GopYPhanMemsController GopYPhanMemService { get { return new GopYPhanMemsController(); } }
        public static DapAnsController DapAnService { get { return new DapAnsController(); } }
    }
}
