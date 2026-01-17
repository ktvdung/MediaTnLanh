import Api, { IApi } from '../models/Api';
import { BaseService } from './BaseService';
import crypto from 'crypto';

class ApiService extends BaseService<IApi> {
  constructor() {
    super(Api);
  }

  /**
   * Generate a unique API key
   */
  generateApiKey(): string {
    const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let result = '';
    for (let i = 0; i < 32; i++) {
      result += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return result;
  }

  /**
   * Create a new API key with auto-generated key value
   */
  async createWithGeneratedKey(ipTao: string): Promise<IApi> {
    const apiKey = this.generateApiKey();
    return this.create({
      _id: crypto.randomUUID(),
      API: apiKey,
      IPTao: ipTao,
      NgayTao: new Date(),
      TrangThai: true,
    } as Partial<IApi>);
  }

  /**
   * Validate an API key
   */
  async validateKey(apiKey: string): Promise<boolean> {
    const api = await this.findOne({ API: apiKey, TrangThai: true });
    return api !== null;
  }

  /**
   * Deactivate an API key
   */
  async deactivate(id: string): Promise<IApi | null> {
    return this.update(id, { TrangThai: false });
  }

  /**
   * Activate an API key
   */
  async activate(id: string): Promise<IApi | null> {
    return this.update(id, { TrangThai: true });
  }
}

export const apiService = new ApiService();
export default apiService;
