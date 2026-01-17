import { Router } from 'express';
import tienToController from '../controllers/TienToController';

const router = Router();
router.get('/', tienToController.getAll);
router.get('/:id', tienToController.getById);
router.post('/', tienToController.create);
router.put('/:id', tienToController.update);
router.delete('/:id', tienToController.delete);

export default router;
