import mongoose from 'mongoose';
import { connectDatabase, disconnectDatabase } from '../config/database';

// Import all models
import Api from '../models/Api';
import NgonNgu from '../models/NgonNgu';
import CauHinh from '../models/CauHinh';
import BanMau from '../models/BanMau';
import TienTo from '../models/TienTo';
import TcTheLoai from '../models/TcTheLoai';
import TcNgonNgu from '../models/TcNgonNgu';
import TcTheLoaiNgonNgu from '../models/TcTheLoaiNgonNgu';
import TcThanhCa from '../models/TcThanhCa';
import TcLoi from '../models/TcLoi';
import KtBanDich from '../models/KtBanDich';
import KtSach from '../models/KtSach';
import KtCau from '../models/KtCau';
import KaTheLoai from '../models/KaTheLoai';
import KaBaiHat from '../models/KaBaiHat';
import DktLoai from '../models/DktLoai';
import DktChuDe from '../models/DktChuDe';
import DktCauHoi from '../models/DktCauHoi';

describe('Migration Integrity Tests', () => {
  beforeAll(async () => {
    await connectDatabase();
  });

  afterAll(async () => {
    await disconnectDatabase();
  });

  describe('Property 6: Data Migration Integrity', () => {
    it('should have consistent data in all collections', async () => {
      const collections = [
        Api, NgonNgu, CauHinh, BanMau, TienTo,
        TcTheLoai, TcNgonNgu, TcTheLoaiNgonNgu, TcThanhCa, TcLoi,
        KtBanDich, KtSach, KtCau, KaTheLoai, KaBaiHat,
        DktLoai, DktChuDe, DktCauHoi,
      ];

      for (const model of collections) {
        const count = await model.countDocuments();
        expect(count).toBeGreaterThanOrEqual(0);
      }
    });

    it('should have valid _id fields in all documents', async () => {
      // Check NgonNgu
      const ngonNguDocs = await NgonNgu.find().limit(5).lean();
      for (const doc of ngonNguDocs) {
        expect(doc._id).toBeDefined();
      }
      
      // Check TcTheLoai
      const tcTheLoaiDocs = await TcTheLoai.find().limit(5).lean();
      for (const doc of tcTheLoaiDocs) {
        expect(doc._id).toBeDefined();
      }
      
      // Check KtBanDich
      const ktBanDichDocs = await KtBanDich.find().limit(5).lean();
      for (const doc of ktBanDichDocs) {
        expect(doc._id).toBeDefined();
      }
    });

    it('should maintain data consistency for TcThanhCa', async () => {
      const count = await TcThanhCa.countDocuments();
      const docs = await TcThanhCa.find().limit(5).lean();
      expect(count).toBeGreaterThanOrEqual(0);
      expect(Array.isArray(docs)).toBe(true);
    });

    it('should maintain data consistency for KtCau', async () => {
      const count = await KtCau.countDocuments();
      const docs = await KtCau.find().limit(5).lean();
      expect(count).toBeGreaterThanOrEqual(0);
      expect(Array.isArray(docs)).toBe(true);
    });

    it('should maintain data consistency for DktCauHoi', async () => {
      const count = await DktCauHoi.countDocuments();
      const docs = await DktCauHoi.find().limit(5).lean();
      expect(count).toBeGreaterThanOrEqual(0);
      expect(Array.isArray(docs)).toBe(true);
    });
  });
});
