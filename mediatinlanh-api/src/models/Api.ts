import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IApi {
  _id: string;
  API: string;
  IPTao: string;
  NgayTao: Date;
  TrangThai: boolean;
}

const generateUUID = (): string => {
  return crypto.randomUUID();
};

const ApiSchema = new Schema<IApi>(
  {
    _id: {
      type: String,
      default: generateUUID,
    },
    API: {
      type: String,
      unique: true,
      index: true,
    },
    IPTao: {
      type: String,
      default: '',
    },
    NgayTao: {
      type: Date,
      default: Date.now,
    },
    TrangThai: {
      type: Boolean,
      default: true,
    },
  },
  {
    timestamps: false,
    versionKey: false,
    strict: false,
  }
);

// Generate unique API key
ApiSchema.statics.generateApiKey = (): string => {
  const chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
  let result = '';
  for (let i = 0; i < 32; i++) {
    result += chars.charAt(Math.floor(Math.random() * chars.length));
  }
  return result;
};

const Api = mongoose.model<IApi>('Api', ApiSchema, 'Apis');

export default Api;
