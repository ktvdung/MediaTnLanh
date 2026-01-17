import { Router } from 'express';
import cauHinhController from '../controllers/CauHinhController';

const router = Router();
router.get('/', cauHinhController.getAll);
router.get('/:id', cauHinhController.getById);
router.post('/', cauHinhController.create);
router.put('/:id', cauHinhController.update);
router.delete('/:id', cauHinhController.delete);

export default router;
