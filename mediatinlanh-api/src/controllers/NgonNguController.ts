import NgonNgu, { INgonNgu } from '../models/NgonNgu';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class NgonNguController extends BaseController<INgonNgu> {
  constructor() {
    super(new BaseService<INgonNgu>(NgonNgu), 'NgonNgu');
  }
}

export const ngonNguController = new NgonNguController();
export default ngonNguController;
