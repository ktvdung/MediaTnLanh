import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ICapNhat {
  _id: string;
  PhienBan: string;
  MoTa: string;
  NgayCapNhat: Date;
}

const CapNhatSchema = new Schema<ICapNhat>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    PhienBan: { type: String },
    MoTa: { type: String, default: '' },
    NgayCapNhat: { type: Date, default: Date.now },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const CapNhat = mongoose.model<ICapNhat>('CapNhat', CapNhatSchema, 'CapNhats');
export default CapNhat;
