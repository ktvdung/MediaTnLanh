import { Router } from 'express';
import apiController from '../controllers/ApiController';

const router = Router();

// GET /api/Apis - Get all API keys
router.get('/', apiController.getAll);

// GET /api/Apis/:id - Get API key by ID
router.get('/:id', apiController.getById);

// POST /api/Apis - Create new API key
router.post('/', apiController.create);

// PUT /api/Apis/:id - Update API key
router.put('/:id', apiController.update);

// DELETE /api/Apis/:id - Delete API key
router.delete('/:id', apiController.delete);

// POST /api/Apis/:id/deactivate - Deactivate API key
router.post('/:id/deactivate', apiController.deactivate);

// POST /api/Apis/:id/activate - Activate API key
router.post('/:id/activate', apiController.activate);

export default router;
