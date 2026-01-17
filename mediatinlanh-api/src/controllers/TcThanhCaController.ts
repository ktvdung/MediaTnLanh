import { Request, Response } from 'express';
import TcThanhCa, { ITcThanhCa } from '../models/TcThanhCa';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TcThanhCaController extends BaseController<ITcThanhCa> {
  constructor() {
    super(new BaseService<ITcThanhCa>(TcThanhCa), 'TcThanhCa');
  }

  getAll = async (req: Request, res: Response): Promise<void> => {
    try {
      const page = parseInt(req.query.page as string) || 1;
      const perPage = parseInt(req.query.per_page as string) || 50;
      const theLoai = req.query.theLoai as string;
      const ngonNgu = req.query.ngonNgu as string;

      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      const filter: any = {};
      if (theLoai) filter.ID_TC_TheLoai = theLoai;
      if (ngonNgu) filter.ID_TC_NgonNgu = ngonNgu;

      const result = await this.service.findAll(filter, { page, perPage });
      res.json({ success: true, ...result });
    } catch (error) {
      console.error('Error getting TcThanhCa:', error);
      res.status(500).json({ success: false, error: { code: 'INTERNAL_SERVER_ERROR', message: 'Failed to get TcThanhCa' } });
    }
  };
}

export const tcThanhCaController = new TcThanhCaController();
export default tcThanhCaController;
