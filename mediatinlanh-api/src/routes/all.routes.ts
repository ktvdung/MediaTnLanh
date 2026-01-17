import { Router } from 'express';
import { BaseController } from '../controllers/BaseController';
import { BaseService } from '../services/BaseService';

// Import all models
import KtBanDich, { IKtBanDich } from '../models/KtBanDich';
import KtSach, { IKtSach } from '../models/KtSach';
import KtCau, { IKtCau } from '../models/KtCau';
import KaTheLoai, { IKaTheLoai } from '../models/KaTheLoai';
import KaBaiHat, { IKaBaiHat } from '../models/KaBaiHat';
import DktLoai, { IDktLoai } from '../models/DktLoai';
import DktChuDe, { IDktChuDe } from '../models/DktChuDe';
import DktLoaiNgonNgu, { IDktLoaiNgonNgu } from '../models/DktLoaiNgonNgu';
import DktChuDeNgonNgu, { IDktChuDeNgonNgu } from '../models/DktChuDeNgonNgu';
import DktCauHoi, { IDktCauHoi } from '../models/DktCauHoi';
import NguoiDung, { INguoiDung } from '../models/NguoiDung';
import Tag, { ITag } from '../models/Tag';
import DoiTuong, { IDoiTuong } from '../models/DoiTuong';
import TepTinTheLoai, { ITepTinTheLoai } from '../models/TepTinTheLoai';
import TepTin, { ITepTin } from '../models/TepTin';
import HuongDan, { IHuongDan } from '../models/HuongDan';
import NhatKy, { INhatKy } from '../models/NhatKy';
import CapNhat, { ICapNhat } from '../models/CapNhat';
import FilesServer, { IFilesServer } from '../models/FilesServer';

// Helper to create CRUD routes
function createCrudRoutes<T>(model: any, name: string): Router {
  const router = Router();
  const controller = new BaseController<T>(new BaseService<T>(model), name);
  
  router.get('/', controller.getAll);
  router.get('/:id', controller.getById);
  router.post('/', controller.create);
  router.put('/:id', controller.update);
  router.delete('/:id', controller.delete);
  
  return router;
}

// Export all routes
export const ktBanDichRoutes = createCrudRoutes<IKtBanDich>(KtBanDich, 'KtBanDich');
export const ktSachRoutes = createCrudRoutes<IKtSach>(KtSach, 'KtSach');
export const ktCauRoutes = createCrudRoutes<IKtCau>(KtCau, 'KtCau');
export const kaTheLoaiRoutes = createCrudRoutes<IKaTheLoai>(KaTheLoai, 'KaTheLoai');
export const kaBaiHatRoutes = createCrudRoutes<IKaBaiHat>(KaBaiHat, 'KaBaiHat');
export const dktLoaiRoutes = createCrudRoutes<IDktLoai>(DktLoai, 'DktLoai');
export const dktChuDeRoutes = createCrudRoutes<IDktChuDe>(DktChuDe, 'DktChuDe');
export const dktLoaiNgonNguRoutes = createCrudRoutes<IDktLoaiNgonNgu>(DktLoaiNgonNgu, 'DktLoaiNgonNgu');
export const dktChuDeNgonNguRoutes = createCrudRoutes<IDktChuDeNgonNgu>(DktChuDeNgonNgu, 'DktChuDeNgonNgu');
export const dktCauHoiRoutes = createCrudRoutes<IDktCauHoi>(DktCauHoi, 'DktCauHoi');
export const nguoiDungRoutes = createCrudRoutes<INguoiDung>(NguoiDung, 'NguoiDung');
export const tagRoutes = createCrudRoutes<ITag>(Tag, 'Tag');
export const doiTuongRoutes = createCrudRoutes<IDoiTuong>(DoiTuong, 'DoiTuong');
export const tepTinTheLoaiRoutes = createCrudRoutes<ITepTinTheLoai>(TepTinTheLoai, 'TepTinTheLoai');
export const tepTinRoutes = createCrudRoutes<ITepTin>(TepTin, 'TepTin');
export const huongDanRoutes = createCrudRoutes<IHuongDan>(HuongDan, 'HuongDan');
export const nhatKyRoutes = createCrudRoutes<INhatKy>(NhatKy, 'NhatKy');
export const capNhatRoutes = createCrudRoutes<ICapNhat>(CapNhat, 'CapNhat');
export const filesServerRoutes = createCrudRoutes<IFilesServer>(FilesServer, 'FilesServer');
