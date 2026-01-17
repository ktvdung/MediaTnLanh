import { Request, Response } from 'express';
import { BaseService, QueryOptions } from '../services/BaseService';

export class BaseController<T> {
  protected service: BaseService<T>;
  protected resourceName: string;

  constructor(service: BaseService<T>, resourceName: string) {
    this.service = service;
    this.resourceName = resourceName;
  }

  /**
   * Get all resources with pagination
   */
  getAll = async (req: Request, res: Response): Promise<void> => {
    try {
      const page = parseInt(req.query.page as string) || 1;
      const perPage = parseInt(req.query.per_page as string) || 50;
      const sort = req.query.sort as string;

      const options: QueryOptions = {
        page,
        perPage,
      };

      if (sort) {
        const sortOrder = sort.startsWith('-') ? -1 : 1;
        const sortField = sort.replace(/^-/, '');
        options.sort = { [sortField]: sortOrder };
      }

      const result = await this.service.findAll({}, options);

      res.json({
        success: true,
        ...result,
      });
    } catch (error) {
      console.error(`Error getting ${this.resourceName}:`, error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: `Failed to get ${this.resourceName}`,
        },
      });
    }
  };

  /**
   * Get resource by ID
   */
  getById = async (req: Request, res: Response): Promise<void> => {
    try {
      const id = req.params.id as string;
      const resource = await this.service.findById(id);

      if (!resource) {
        res.status(404).json({
          success: false,
          error: {
            code: 'NOT_FOUND',
            message: `${this.resourceName} not found`,
          },
        });
        return;
      }

      res.json({
        success: true,
        data: resource,
      });
    } catch (error) {
      console.error(`Error getting ${this.resourceName}:`, error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: `Failed to get ${this.resourceName}`,
        },
      });
    }
  };

  /**
   * Create new resource
   */
  create = async (req: Request, res: Response): Promise<void> => {
    try {
      const resource = await this.service.create(req.body);

      res.status(201).json({
        success: true,
        data: resource,
      });
    } catch (error) {
      console.error(`Error creating ${this.resourceName}:`, error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: `Failed to create ${this.resourceName}`,
        },
      });
    }
  };

  /**
   * Update resource
   */
  update = async (req: Request, res: Response): Promise<void> => {
    try {
      const id = req.params.id as string;
      const resource = await this.service.update(id, req.body);

      if (!resource) {
        res.status(404).json({
          success: false,
          error: {
            code: 'NOT_FOUND',
            message: `${this.resourceName} not found`,
          },
        });
        return;
      }

      res.json({
        success: true,
        data: resource,
      });
    } catch (error) {
      console.error(`Error updating ${this.resourceName}:`, error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: `Failed to update ${this.resourceName}`,
        },
      });
    }
  };

  /**
   * Delete resource
   */
  delete = async (req: Request, res: Response): Promise<void> => {
    try {
      const id = req.params.id as string;
      const deleted = await this.service.delete(id);

      if (!deleted) {
        res.status(404).json({
          success: false,
          error: {
            code: 'NOT_FOUND',
            message: `${this.resourceName} not found`,
          },
        });
        return;
      }

      res.status(204).send();
    } catch (error) {
      console.error(`Error deleting ${this.resourceName}:`, error);
      res.status(500).json({
        success: false,
        error: {
          code: 'INTERNAL_SERVER_ERROR',
          message: `Failed to delete ${this.resourceName}`,
        },
      });
    }
  };
}

export default BaseController;
