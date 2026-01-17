import BanMau, { IBanMau } from '../models/BanMau';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class BanMauController extends BaseController<IBanMau> {
  constructor() {
    super(new BaseService<IBanMau>(BanMau), 'BanMau');
  }
}

export const banMauController = new BanMauController();
export default banMauController;
