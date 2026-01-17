import express, { Application, Request, Response, NextFunction } from 'express';
import cors from 'cors';
import helmet from 'helmet';
import morgan from 'morgan';
import dotenv from 'dotenv';

import { connectDatabase, isDatabaseConnected } from './config/database';
import config from './config';
import routes from './routes';

// Load environment variables
dotenv.config();

// Create Express app
const app: Application = express();

// Middleware
app.use(helmet());
app.use(cors({
  origin: '*',
  methods: ['GET', 'POST', 'PUT', 'DELETE', 'PATCH', 'OPTIONS'],
  allowedHeaders: ['Content-Type', 'Authorization', 'X-API-KEY'],
}));
app.use(morgan('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

// Health check endpoint (no auth required)
app.get(`${config.apiPrefix}/health`, (_req: Request, res: Response) => {
  const status = isDatabaseConnected() ? 'ok' : 'error';
  const details = isDatabaseConnected() 
    ? { database: 'connected' } 
    : { database: 'disconnected' };
  
  res.status(status === 'ok' ? 200 : 503).json({
    status,
    timestamp: new Date().toISOString(),
    ...details,
  });
});

// API routes
app.use(config.apiPrefix, routes);

// Error handling middleware
app.use((err: Error, _req: Request, res: Response, _next: NextFunction) => {
  console.error('Error:', err);
  res.status(500).json({
    success: false,
    error: {
      code: 'INTERNAL_SERVER_ERROR',
      message: config.nodeEnv === 'production' 
        ? 'An unexpected error occurred' 
        : err.message,
    },
  });
});

// 404 handler
app.use((_req: Request, res: Response) => {
  res.status(404).json({
    success: false,
    error: {
      code: 'NOT_FOUND',
      message: 'Resource not found',
    },
  });
});

// Start server
const startServer = async (): Promise<void> => {
  try {
    // Connect to MongoDB
    await connectDatabase();
    
    // Start Express server
    app.listen(config.port, () => {
      console.log(`🚀 Server running on port ${config.port}`);
      console.log(`📍 API available at http://localhost:${config.port}${config.apiPrefix}`);
      console.log(`🏥 Health check at http://localhost:${config.port}${config.apiPrefix}/health`);
    });
  } catch (error) {
    console.error('Failed to start server:', error);
    process.exit(1);
  }
};

// Only start server if this file is run directly
if (require.main === module) {
  startServer();
}

export { app, startServer };
