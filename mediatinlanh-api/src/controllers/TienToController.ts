import TienTo, { ITienTo } from '../models/TienTo';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TienToController extends BaseController<ITienTo> {
  constructor() {
    super(new BaseService<ITienTo>(TienTo), 'TienTo');
  }
}

export const tienToController = new TienToController();
export default tienToController;
