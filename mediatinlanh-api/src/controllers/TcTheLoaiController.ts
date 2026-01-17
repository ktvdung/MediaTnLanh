import TcTheLoai, { ITcTheLoai } from '../models/TcTheLoai';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TcTheLoaiController extends BaseController<ITcTheLoai> {
  constructor() {
    super(new BaseService<ITcTheLoai>(TcTheLoai), 'TcTheLoai');
  }
}

export const tcTheLoaiController = new TcTheLoaiController();
export default tcTheLoaiController;
