import mongoose from 'mongoose';
import fc from 'fast-check';
import { connectDatabase } from '../config/database';
import { BaseService } from '../services/BaseService';

// Import all models
import TcThanhCa from '../models/TcThanhCa';
import TcTheLoai from '../models/TcTheLoai';
import KtSach from '../models/KtSach';
import KtCau from '../models/KtCau';
import KaBaiHat from '../models/KaBaiHat';
import DktCauHoi from '../models/DktCauHoi';
import NguoiDung from '../models/NguoiDung';
import TepTin from '../models/TepTin';
import HuongDan from '../models/HuongDan';

// Store created IDs for cleanup
const createdIds: { model: mongoose.Model<any>; id: string }[] = [];

describe('All Modules Property Tests', () => {
  beforeAll(async () => {
    await connectDatabase();
  }, 30000);

  afterAll(async () => {
    // Clean up only test data
    for (const { model, id } of createdIds) {
      try {
        await model.deleteOne({ _id: id });
      } catch (e) {
        // Ignore errors
      }
    }
    await mongoose.connection.close();
  }, 30000);

  /**
   * Feature: nodejs-mongodb-vue, Property 3: CRUD Round-Trip Consistency
   * Validates: Requirements 4.1-4.8, 5.1-5.6, 6.1-6.4, 7.1-7.5
   */
  describe('Property 3: CRUD Round-Trip for Content Modules', () => {
    it('TcThanhCa: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(TcThanhCa);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            So: fc.integer({ min: 1, max: 1000 }),
            ThanhCaTen: fc.string({ minLength: 1, maxLength: 100 }),
            ID_TC_TheLoai: fc.string({ minLength: 1, maxLength: 36 }),
          }),
          async (data) => {
            const created = await service.create(data);
            createdIds.push({ model: TcThanhCa, id: created._id });
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.So).toBe(data.So);
            expect(retrieved!.ThanhCaTen).toBe(data.ThanhCaTen);
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);

    it('KtSach: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(KtSach);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            TenSach: fc.string({ minLength: 1, maxLength: 50 }),
            SoThuTu: fc.integer({ min: 1, max: 66 }),
          }),
          async (data) => {
            const created = await service.create(data);
            createdIds.push({ model: KtSach, id: created._id });
            const retrieved = await service.findById(created._id) as any;
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.TenSach).toBe(data.TenSach);
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);

    it('KaBaiHat: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(KaBaiHat);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            BaiHatTen: fc.string({ minLength: 1, maxLength: 100 }),
            TacGia: fc.string({ minLength: 0, maxLength: 50 }),
          }),
          async (data) => {
            const created = await service.create(data);
            createdIds.push({ model: KaBaiHat, id: created._id });
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.BaiHatTen).toBe(data.BaiHatTen);
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);

    it('DktCauHoi: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(DktCauHoi);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            Question: fc.string({ minLength: 1, maxLength: 200 }),
            AnswerA: fc.string({ minLength: 0, maxLength: 200 }),
            CorrectIndex: fc.integer({ min: 1, max: 4 }),
          }),
          async (data) => {
            const created = await service.create(data);
            createdIds.push({ model: DktCauHoi, id: created._id });
            const retrieved = await service.findById(created._id) as any;
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.Question).toBe(data.Question);
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);
  });

  /**
   * Feature: nodejs-mongodb-vue, Property 7: Delete Consistency
   * Validates: Requirements for delete operations
   */
  describe('Property 7: Delete Consistency', () => {
    it('deleted entity should return null on retrieval', async () => {
      const service = new BaseService(HuongDan);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            TieuDe: fc.string({ minLength: 1, maxLength: 50 }),
            NoiDung: fc.string({ minLength: 0, maxLength: 200 }),
          }),
          async (data) => {
            const created = await service.create(data);
            await service.delete(created._id);
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).toBeNull();
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);
  });

  /**
   * Feature: nodejs-mongodb-vue, Property 8: Update Idempotence
   * Validates: Requirements for update operations
   */
  describe('Property 8: Update Idempotence', () => {
    it('updating with same data twice should produce identical results', async () => {
      const service = new BaseService(TepTin);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            TenTepTin: fc.string({ minLength: 1, maxLength: 50 }),
            UrlTep: fc.string({ minLength: 0, maxLength: 100 }),
          }),
          async (data) => {
            const created = await service.create(data);
            createdIds.push({ model: TepTin, id: created._id });
            const updateData = { TenTepTin: 'Updated Name' };
            
            const firstUpdate = await service.update(created._id, updateData) as any;
            const secondUpdate = await service.update(created._id, updateData) as any;
            
            expect(firstUpdate!.TenTepTin).toBe(secondUpdate!.TenTepTin);
          }
        ),
        { numRuns: 5 }
      );
    }, 30000);
  });
});
