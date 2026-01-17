import TcNgonNgu, { ITcNgonNgu } from '../models/TcNgonNgu';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TcNgonNguController extends BaseController<ITcNgonNgu> {
  constructor() {
    super(new BaseService<ITcNgonNgu>(TcNgonNgu), 'TcNgonNgu');
  }
}

export const tcNgonNguController = new TcNgonNguController();
export default tcNgonNguController;
