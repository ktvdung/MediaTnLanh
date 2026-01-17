import { Router } from 'express';
import tcThanhCaController from '../controllers/TcThanhCaController';
import tcTheLoaiController from '../controllers/TcTheLoaiController';
import tcNgonNguController from '../controllers/TcNgonNguController';
import tcTheLoaiNgonNguController from '../controllers/TcTheLoaiNgonNguController';
import tcLoiController from '../controllers/TcLoiController';

const router = Router();

// TcThanhCa routes
router.get('/', tcThanhCaController.getAll);
router.get('/:id', tcThanhCaController.getById);
router.post('/', tcThanhCaController.create);
router.put('/:id', tcThanhCaController.update);
router.delete('/:id', tcThanhCaController.delete);

export const tcTheLoaiRouter = Router();
tcTheLoaiRouter.get('/', tcTheLoaiController.getAll);
tcTheLoaiRouter.get('/:id', tcTheLoaiController.getById);
tcTheLoaiRouter.post('/', tcTheLoaiController.create);
tcTheLoaiRouter.put('/:id', tcTheLoaiController.update);
tcTheLoaiRouter.delete('/:id', tcTheLoaiController.delete);

export const tcNgonNguRouter = Router();
tcNgonNguRouter.get('/', tcNgonNguController.getAll);
tcNgonNguRouter.get('/:id', tcNgonNguController.getById);
tcNgonNguRouter.post('/', tcNgonNguController.create);
tcNgonNguRouter.put('/:id', tcNgonNguController.update);
tcNgonNguRouter.delete('/:id', tcNgonNguController.delete);

export const tcTheLoaiNgonNguRouter = Router();
tcTheLoaiNgonNguRouter.get('/', tcTheLoaiNgonNguController.getAll);
tcTheLoaiNgonNguRouter.get('/:id', tcTheLoaiNgonNguController.getById);
tcTheLoaiNgonNguRouter.post('/', tcTheLoaiNgonNguController.create);
tcTheLoaiNgonNguRouter.put('/:id', tcTheLoaiNgonNguController.update);
tcTheLoaiNgonNguRouter.delete('/:id', tcTheLoaiNgonNguController.delete);

export const tcLoiRouter = Router();
tcLoiRouter.get('/', tcLoiController.getAll);
tcLoiRouter.get('/:id', tcLoiController.getById);
tcLoiRouter.post('/', tcLoiController.create);
tcLoiRouter.put('/:id', tcLoiController.update);
tcLoiRouter.delete('/:id', tcLoiController.delete);

export default router;
