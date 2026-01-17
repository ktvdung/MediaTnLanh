import TcTheLoaiNgonNgu, { ITcTheLoaiNgonNgu } from '../models/TcTheLoaiNgonNgu';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TcTheLoaiNgonNguController extends BaseController<ITcTheLoaiNgonNgu> {
  constructor() {
    super(new BaseService<ITcTheLoaiNgonNgu>(TcTheLoaiNgonNgu), 'TcTheLoaiNgonNgu');
  }
}

export const tcTheLoaiNgonNguController = new TcTheLoaiNgonNguController();
export default tcTheLoaiNgonNguController;
