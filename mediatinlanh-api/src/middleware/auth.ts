import { Request, Response, NextFunction } from 'express';
import Api from '../models/Api';

export interface AuthenticatedRequest extends Request {
  apiKey?: string;
}

/**
 * Middleware to validate API key from X-API-KEY header or api_key query parameter
 */
export const validateApiKey = async (
  req: AuthenticatedRequest,
  res: Response,
  next: NextFunction
): Promise<void> => {
  try {
    // Get API key from header or query parameter
    const apiKey = req.headers['x-api-key'] as string || req.query.api_key as string;

    if (!apiKey) {
      res.status(401).json({
        success: false,
        error: {
          code: 'UNAUTHORIZED',
          message: 'API key is required',
        },
      });
      return;
    }

    // Validate API key exists in database
    const apiRecord = await Api.findOne({ API: apiKey, TrangThai: true });

    if (!apiRecord) {
      res.status(401).json({
        success: false,
        error: {
          code: 'UNAUTHORIZED',
          message: 'Invalid API key',
        },
      });
      return;
    }

    // Attach API key to request for later use
    req.apiKey = apiKey;
    next();
  } catch (error) {
    console.error('Auth middleware error:', error);
    res.status(500).json({
      success: false,
      error: {
        code: 'INTERNAL_SERVER_ERROR',
        message: 'Authentication error',
      },
    });
  }
};

/**
 * Optional API key validation - allows requests without API key but attaches it if present
 */
export const optionalApiKey = async (
  req: AuthenticatedRequest,
  _res: Response,
  next: NextFunction
): Promise<void> => {
  const apiKey = req.headers['x-api-key'] as string || req.query.api_key as string;
  
  if (apiKey) {
    req.apiKey = apiKey;
  }
  
  next();
};

export default validateApiKey;
