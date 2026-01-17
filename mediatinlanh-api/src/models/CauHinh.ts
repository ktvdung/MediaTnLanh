import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ICauHinh {
  _id: string;
  Ten?: string;
  GiaTri?: string;
  Autoload?: string;
}

const CauHinhSchema = new Schema<ICauHinh>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    Ten: { type: String },
    GiaTri: { type: String },
    Autoload: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const CauHinh = mongoose.model<ICauHinh>('CauHinh', CauHinhSchema, 'CauHinhs');
export default CauHinh;
