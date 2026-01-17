import { Router } from 'express';
import ngonNguController from '../controllers/NgonNguController';

const router = Router();
router.get('/', ngonNguController.getAll);
router.get('/:id', ngonNguController.getById);
router.post('/', ngonNguController.create);
router.put('/:id', ngonNguController.update);
router.delete('/:id', ngonNguController.delete);

export default router;
