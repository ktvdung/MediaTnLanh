using AutoMapper;
using MediaTinLanh.Data;
using MediaTinLanh.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTinLanh.Control
{
    public class BaseController
    {
        static bool initialized = false;
        static BaseController()
        {

            if (!initialized)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });

                initialized = true;
            }
        }
    }

    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<NgonNgu, NgonNguModel>();
            CreateMap<ThanhCa, ThanhCaModel>().ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai.HasValue && src.TrangThai.Value == 1 ? true : false)).ForMember(dest => dest.LoaiThanhCa, conf => conf.MapFrom(src => src.LoaiThanhCa)).ForMember(dest => dest.LoiBaiHats, opt => opt.MapFrom(src => src.DanhSachLoiBaiHat)).ForMember(dest => dest.Medias, opt => opt.MapFrom(src => src.DanhSachMedia));

            CreateMap<LoiBaiHat, LoiBaiHatModel>();
            CreateMap<CauKinhThanh, CauKinhThanhModel>();
            CreateMap<BanDichCau, BanDichCauModel>();
            CreateMap<BanDichPhienBan, BanDichPhienBanModel>();
            CreateMap<BanDichSach, BanDichSachModel>();
            CreateMap<Template, TemplateModel>();
            CreateMap<Media, MediaModel>();
            CreateMap<MediaThanhCa, MediaThanhCaModel>();
            CreateMap<MediaType, MediaTypeModel>();
            CreateMap<ChuDe, ChuDeModel>();
            CreateMap<CauDo, CauDoModel>();
            CreateMap<DapAn, DapAnModel>();
            CreateMap<CauHoi, CauHoiModel>();
            CreateMap<Sach, SachModel>();
            CreateMap<PhienBan, PhienBanModel>();
            CreateMap<BoDe, BoDe>();
            CreateMap<GopYPhanMem, GopYPhanMemModel>();
            CreateMap<LoaiThanhCa, LoaiThanhCaModel>();

            CreateMap<NgonNguModel, NgonNgu>();
            CreateMap<ThanhCaModel, ThanhCa>().ForMember(dest => dest.TrangThai, opt => opt.MapFrom(src => src.TrangThai.HasValue && src.TrangThai.Value == true ? 1 : 0));
            CreateMap<LoiBaiHatModel, LoiBaiHat>();
            CreateMap<CauKinhThanhModel, CauKinhThanh>();
            CreateMap<BanDichCauModel, BanDichCau>();
            CreateMap<BanDichPhienBanModel, BanDichPhienBan>();
            CreateMap<BanDichSachModel, BanDichSach>();
            CreateMap<TemplateModel, Template>();
            CreateMap<MediaModel, Media>();
            CreateMap<MediaThanhCaModel, MediaThanhCa>();
            CreateMap<MediaTypeModel, MediaType>();
            CreateMap<ChuDeModel, ChuDe>();
            CreateMap<CauDoModel, CauDo>();
            CreateMap<DapAnModel, DapAn>();
            CreateMap<CauHoiModel, CauHoi>();
            CreateMap<SachModel, Sach>();
            CreateMap<PhienBanModel, PhienBan>();
            CreateMap<BoDeModel, BoDe>();
            CreateMap<GopYPhanMemModel, GopYPhanMem>();
            CreateMap<LoaiThanhCaModel, LoaiThanhCa>();
        }
    }
}
