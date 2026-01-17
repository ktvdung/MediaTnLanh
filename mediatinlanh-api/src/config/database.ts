import mongoose from 'mongoose';
import dotenv from 'dotenv';

dotenv.config();

interface DatabaseConfig {
  uri: string;
  options: mongoose.ConnectOptions;
}

const getDatabaseConfig = (): DatabaseConfig => {
  const uri = process.env.MONGODB_URI;
  
  if (!uri) {
    throw new Error('MONGODB_URI environment variable is not defined');
  }

  return {
    uri,
    options: {
      // MongoDB driver options
    },
  };
};

export const connectDatabase = async (): Promise<void> => {
  try {
    const config = getDatabaseConfig();
    
    mongoose.connection.on('connected', () => {
      console.log('✅ MongoDB connected successfully');
    });

    mongoose.connection.on('error', (err) => {
      console.error('❌ MongoDB connection error:', err);
    });

    mongoose.connection.on('disconnected', () => {
      console.log('⚠️ MongoDB disconnected');
    });

    await mongoose.connect(config.uri, config.options);
  } catch (error) {
    console.error('❌ Failed to connect to MongoDB:', error);
    throw error;
  }
};

export const disconnectDatabase = async (): Promise<void> => {
  try {
    await mongoose.connection.close();
    console.log('MongoDB connection closed');
  } catch (error) {
    console.error('Error closing MongoDB connection:', error);
    throw error;
  }
};

export const isDatabaseConnected = (): boolean => {
  return mongoose.connection.readyState === 1;
};

export default {
  connectDatabase,
  disconnectDatabase,
  isDatabaseConnected,
};
