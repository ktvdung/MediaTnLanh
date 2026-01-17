import TcLoi, { ITcLoi } from '../models/TcLoi';
import { BaseController } from './BaseController';
import { BaseService } from '../services/BaseService';

class TcLoiController extends BaseController<ITcLoi> {
  constructor() {
    super(new BaseService<ITcLoi>(TcLoi), 'TcLoi');
  }
}

export const tcLoiController = new TcLoiController();
export default tcLoiController;
