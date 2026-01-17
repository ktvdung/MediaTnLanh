import mongoose from 'mongoose';
import fc from 'fast-check';
import { connectDatabase } from '../config/database';
import NgonNgu from '../models/NgonNgu';
import CauHinh from '../models/CauHinh';
import BanMau from '../models/BanMau';
import TienTo from '../models/TienTo';
import { BaseService } from '../services/BaseService';

describe('CRUD Round-Trip Property Tests', () => {
  beforeAll(async () => {
    await connectDatabase();
  });

  afterAll(async () => {
    await mongoose.connection.close();
  });

  afterEach(async () => {
    await NgonNgu.deleteMany({});
    await CauHinh.deleteMany({});
    await BanMau.deleteMany({});
    await TienTo.deleteMany({});
  });

  /**
   * Feature: nodejs-mongodb-vue, Property 3: CRUD Round-Trip Consistency
   * Validates: Requirements 3.1-3.6, 13.1-13.2, 14.1, 20.1
   * 
   * For any entity type, creating an entity and then retrieving it by ID
   * SHALL return an entity with equivalent data to what was created.
   */
  describe('Property 3: CRUD Round-Trip Consistency', () => {
    it('NgonNgu: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(NgonNgu);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            NgonNguTen: fc.string({ minLength: 1, maxLength: 50 }),
            NgonNguMa: fc.string({ minLength: 1, maxLength: 10 }),
          }),
          async (data) => {
            const created = await service.create(data);
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.NgonNguTen).toBe(data.NgonNguTen);
            expect(retrieved!.NgonNguMa).toBe(data.NgonNguMa);
          }
        ),
        { numRuns: 20 }
      );
    });

    it('CauHinh: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(CauHinh);
      let counter = 0;
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            GiaTri: fc.string({ minLength: 0, maxLength: 100 }),
          }),
          async (data) => {
            const uniqueKey = `key_${Date.now()}_${counter++}`;
            const created = await service.create({ ...data, Ten: uniqueKey });
            const retrieved = await service.findById(created._id) as any;
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.Ten).toBe(uniqueKey);
            expect(retrieved!.GiaTri).toBe(data.GiaTri);
          }
        ),
        { numRuns: 20 }
      );
    });

    it('BanMau: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(BanMau);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            BanMauTen: fc.string({ minLength: 1, maxLength: 50 }),
            BanMauNoiDung: fc.string({ minLength: 0, maxLength: 200 }),
          }),
          async (data) => {
            const created = await service.create(data);
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.BanMauTen).toBe(data.BanMauTen);
            expect(retrieved!.BanMauNoiDung).toBe(data.BanMauNoiDung);
          }
        ),
        { numRuns: 20 }
      );
    });

    it('TienTo: create then retrieve should return equivalent data', async () => {
      const service = new BaseService(TienTo);
      
      await fc.assert(
        fc.asyncProperty(
          fc.record({
            TienToTen: fc.string({ minLength: 1, maxLength: 50 }),
            TienToGiaTri: fc.string({ minLength: 0, maxLength: 50 }),
          }),
          async (data) => {
            const created = await service.create(data);
            const retrieved = await service.findById(created._id);
            
            expect(retrieved).not.toBeNull();
            expect(retrieved!.TienToTen).toBe(data.TienToTen);
            expect(retrieved!.TienToGiaTri).toBe(data.TienToGiaTri);
          }
        ),
        { numRuns: 20 }
      );
    });
  });
});
