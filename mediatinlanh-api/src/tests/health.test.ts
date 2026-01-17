import request from 'supertest';
import mongoose from 'mongoose';
import { app } from '../app';
import { connectDatabase } from '../config/database';

describe('Health Check Endpoint', () => {
  beforeAll(async () => {
    await connectDatabase();
  });

  afterAll(async () => {
    await mongoose.connection.close();
  });

  /**
   * Validates: Requirements 8.1, 8.2, 8.3
   */
  describe('GET /api/health', () => {
    it('should return status ok when database is connected (no auth required)', async () => {
      const response = await request(app)
        .get('/api/health');

      expect(response.status).toBe(200);
      expect(response.body.status).toBe('ok');
      expect(response.body.timestamp).toBeDefined();
      expect(response.body.database).toBe('connected');
    });

    it('should not require API key authentication', async () => {
      // Request without any API key should still work
      const response = await request(app)
        .get('/api/health');

      expect(response.status).toBe(200);
      expect(response.body.status).toBe('ok');
    });

    it('should return valid ISO timestamp', async () => {
      const response = await request(app)
        .get('/api/health');

      const timestamp = new Date(response.body.timestamp);
      expect(timestamp.toISOString()).toBe(response.body.timestamp);
    });
  });
});
