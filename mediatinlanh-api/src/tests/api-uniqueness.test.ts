import mongoose from 'mongoose';
import fc from 'fast-check';
import Api from '../models/Api';
import apiService from '../services/ApiService';
import { connectDatabase } from '../config/database';

describe('API Key Uniqueness', () => {
  beforeAll(async () => {
    await connectDatabase();
  });

  afterAll(async () => {
    await Api.deleteMany({});
    await mongoose.connection.close();
  });

  afterEach(async () => {
    await Api.deleteMany({});
  });

  /**
   * Feature: nodejs-mongodb-vue, Property 2: API Key Uniqueness
   * Validates: Requirements 2.5
   * 
   * For any two API keys created by the system, their generated key values SHALL be unique.
   */
  describe('Property 2: API Key Uniqueness', () => {
    it('should generate unique API keys for multiple creations', async () => {
      const createdKeys: string[] = [];
      
      // Create multiple API keys and verify uniqueness
      await fc.assert(
        fc.asyncProperty(
          fc.integer({ min: 1, max: 10 }),
          async () => {
            const api = await apiService.createWithGeneratedKey('127.0.0.1');
            
            // Check that this key is not in our list of created keys
            expect(createdKeys).not.toContain(api.API);
            
            // Add to our list
            createdKeys.push(api.API);
            
            return true;
          }
        ),
        { numRuns: 50 }
      );

      // Verify all keys are unique
      const uniqueKeys = new Set(createdKeys);
      expect(uniqueKeys.size).toBe(createdKeys.length);
    });

    it('should generate API keys with sufficient length and randomness', async () => {
      await fc.assert(
        fc.asyncProperty(
          fc.constant(null),
          async () => {
            const api = await apiService.createWithGeneratedKey('127.0.0.1');
            
            // API key should be 32 characters
            expect(api.API.length).toBe(32);
            
            // API key should only contain alphanumeric characters
            expect(api.API).toMatch(/^[A-Za-z0-9]+$/);
            
            return true;
          }
        ),
        { numRuns: 20 }
      );
    });

    it('should store creation IP correctly', async () => {
      await fc.assert(
        fc.asyncProperty(
          fc.ipV4(),
          async (ip) => {
            const api = await apiService.createWithGeneratedKey(ip);
            
            expect(api.IPTao).toBe(ip);
            
            return true;
          }
        ),
        { numRuns: 20 }
      );
    });
  });
});
