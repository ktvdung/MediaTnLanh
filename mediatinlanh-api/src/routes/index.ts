import { Router } from 'express';
import apiRoutes from './api.routes';
import ngonNguRoutes from './ngonngu.routes';
import cauHinhRoutes from './cauhinh.routes';
import banMauRoutes from './banmau.routes';
import tienToRoutes from './tiento.routes';
import tcThanhCaRoutes, { tcTheLoaiRouter, tcNgonNguRouter, tcTheLoaiNgonNguRouter, tcLoiRouter } from './tcthanhca.routes';
import {
  ktBanDichRoutes, ktSachRoutes, ktCauRoutes,
  kaTheLoaiRoutes, kaBaiHatRoutes,
  dktLoaiRoutes, dktChuDeRoutes, dktLoaiNgonNguRoutes, dktChuDeNgonNguRoutes, dktCauHoiRoutes,
  nguoiDungRoutes, tagRoutes, doiTuongRoutes,
  tepTinTheLoaiRoutes, tepTinRoutes,
  huongDanRoutes, nhatKyRoutes, capNhatRoutes, filesServerRoutes
} from './all.routes';
import { validateApiKey } from '../middleware/auth';

const router = Router();

// All routes protected by API key
router.use('/Apis', validateApiKey, apiRoutes);
router.use('/NgonNgus', validateApiKey, ngonNguRoutes);
router.use('/CauHinhs', validateApiKey, cauHinhRoutes);
router.use('/BanMaus', validateApiKey, banMauRoutes);
router.use('/TienTos', validateApiKey, tienToRoutes);

// Thánh Ca routes
router.use('/TcThanhCa', validateApiKey, tcThanhCaRoutes);
router.use('/TcTheLoais', validateApiKey, tcTheLoaiRouter);
router.use('/TcNgonNgus', validateApiKey, tcNgonNguRouter);
router.use('/TcTheLoaiNgonNgus', validateApiKey, tcTheLoaiNgonNguRouter);
router.use('/TcLois', validateApiKey, tcLoiRouter);

// Kinh Thánh routes
router.use('/KtBanDichs', validateApiKey, ktBanDichRoutes);
router.use('/KtSaches', validateApiKey, ktSachRoutes);
router.use('/KtCaus', validateApiKey, ktCauRoutes);

// Karaoke routes
router.use('/KaTheLoais', validateApiKey, kaTheLoaiRoutes);
router.use('/KaBaiHats', validateApiKey, kaBaiHatRoutes);

// Đạo Kinh Thánh routes
router.use('/DktLoais', validateApiKey, dktLoaiRoutes);
router.use('/DktChuDes', validateApiKey, dktChuDeRoutes);
router.use('/DktLoaiNgonNgus', validateApiKey, dktLoaiNgonNguRoutes);
router.use('/DktChuDeNgonNgus', validateApiKey, dktChuDeNgonNguRoutes);
router.use('/DktCauHois', validateApiKey, dktCauHoiRoutes);

// System routes
router.use('/NguoiDungs', validateApiKey, nguoiDungRoutes);
router.use('/Tags', validateApiKey, tagRoutes);
router.use('/DoiTuongs', validateApiKey, doiTuongRoutes);
router.use('/TepTinTheLoais', validateApiKey, tepTinTheLoaiRoutes);
router.use('/TepTins', validateApiKey, tepTinRoutes);
router.use('/HuongDans', validateApiKey, huongDanRoutes);
router.use('/NhatKys', validateApiKey, nhatKyRoutes);
router.use('/CapNhats', validateApiKey, capNhatRoutes);
router.use('/FilesServers', validateApiKey, filesServerRoutes);

export default router;
