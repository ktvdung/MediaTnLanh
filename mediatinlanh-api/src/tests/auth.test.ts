import request from 'supertest';
import mongoose from 'mongoose';
import fc from 'fast-check';
import { app } from '../app';
import Api from '../models/Api';
import { connectDatabase } from '../config/database';

describe('API Key Authentication', () => {
  let validApiKey: string;

  beforeAll(async () => {
    await connectDatabase();
    // Create a valid API key for testing
    const api = await Api.create({
      API: 'test-valid-api-key-12345678901234',
      IPTao: '127.0.0.1',
      TrangThai: true,
    });
    validApiKey = api.API;
  });

  afterAll(async () => {
    await Api.deleteMany({});
    await mongoose.connection.close();
  });

  /**
   * Feature: nodejs-mongodb-vue, Property 1: API Key Authentication Consistency
   * Validates: Requirements 2.1, 2.2, 2.3
   * 
   * For any API request with a valid API key (either in X-API-KEY header or api_key query parameter),
   * the request SHALL be authenticated successfully.
   * For any request without a valid API key, the request SHALL return 401 Unauthorized.
   */
  describe('Property 1: API Key Authentication Consistency', () => {
    it('should authenticate requests with valid API key in header', async () => {
      const response = await request(app)
        .get('/api/Apis')
        .set('X-API-KEY', validApiKey);

      expect(response.status).not.toBe(401);
      expect(response.body.success).toBe(true);
    });

    it('should authenticate requests with valid API key in query parameter', async () => {
      const response = await request(app)
        .get(`/api/Apis?api_key=${validApiKey}`);

      expect(response.status).not.toBe(401);
      expect(response.body.success).toBe(true);
    });

    it('should return 401 for requests without API key', async () => {
      const response = await request(app)
        .get('/api/Apis');

      expect(response.status).toBe(401);
      expect(response.body.success).toBe(false);
      expect(response.body.error.code).toBe('UNAUTHORIZED');
    });

    it('should return 401 for requests with invalid API key', async () => {
      await fc.assert(
        fc.asyncProperty(
          fc.string({ minLength: 1, maxLength: 50 }).filter(s => s !== validApiKey),
          async (invalidKey) => {
            const response = await request(app)
              .get('/api/Apis')
              .set('X-API-KEY', invalidKey);

            expect(response.status).toBe(401);
            expect(response.body.success).toBe(false);
          }
        ),
        { numRuns: 20 }
      );
    });

    it('should return 401 for deactivated API keys', async () => {
      // Create and deactivate an API key
      const deactivatedApi = await Api.create({
        API: 'deactivated-api-key-test123456',
        IPTao: '127.0.0.1',
        TrangThai: false,
      });

      const response = await request(app)
        .get('/api/Apis')
        .set('X-API-KEY', deactivatedApi.API);

      expect(response.status).toBe(401);

      await Api.findByIdAndDelete(deactivatedApi._id);
    });
  });
});
