import { Request, Response } from 'express';
import apiService from '../services/ApiService';
import { BaseController } from './BaseController';
import Api, { IApi } from '../models/Api';
import { BaseService } from '../services/BaseService';

class ApiController extends BaseController<IApi> {
  constructor() {
    super(new BaseService<IApi>(Api), 'Api');
  }

  /**
   * Create new API key with auto-generated key
   */
  create = async (req: Request, res: Response): Promise<void> => {
    try {
      const ipTao = req.ip || (req.headers['x-forwarded-for'] as string) || '';
      const api = await apiService.createWithGeneratedKey(ipTao);

      res.status(201).json({
        success: true,
        data: api,
      });
    } catch (error) {
      console.error('Error creating API key:', error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: 'Failed to create API key',
        },
      });
    }
  };

  /**
   * Deactivate API key
   */
  deactivate = async (req: Request, res: Response): Promise<void> => {
    try {
      const id = req.params.id as string;
      const api = await apiService.deactivate(id);

      if (!api) {
        res.status(404).json({
          success: false,
          error: {
            code: 'NOT_FOUND',
            message: 'API key not found',
          },
        });
        return;
      }

      res.json({
        success: true,
        data: api,
      });
    } catch (error) {
      console.error('Error deactivating API key:', error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: 'Failed to deactivate API key',
        },
      });
    }
  };

  /**
   * Activate API key
   */
  activate = async (req: Request, res: Response): Promise<void> => {
    try {
      const id = req.params.id as string;
      const api = await apiService.activate(id);

      if (!api) {
        res.status(404).json({
          success: false,
          error: {
            code: 'NOT_FOUND',
            message: 'API key not found',
          },
        });
        return;
      }

      res.json({
        success: true,
        data: api,
      });
    } catch (error) {
      console.error('Error activating API key:', error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: 'Failed to activate API key',
        },
      });
    }
  };
}

export const apiController = new ApiController();
export default apiController;
