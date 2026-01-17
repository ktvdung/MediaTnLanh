import CauHinh, { ICauHinh } from '../models/CauHinh';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class CauHinhController extends BaseController<ICauHinh> {
  constructor() {
    super(new BaseService<ICauHinh>(CauHinh), 'CauHinh');
  }
}

export const cauHinhController = new CauHinhController();
export default cauHinhController;
