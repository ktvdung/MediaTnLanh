import { Router } from 'express';
import banMauController from '../controllers/BanMauController';

const router = Router();
router.get('/', banMauController.getAll);
router.get('/:id', banMauController.getById);
router.post('/', banMauController.create);
router.put('/:id', banMauController.update);
router.delete('/:id', banMauController.delete);

export default router;
