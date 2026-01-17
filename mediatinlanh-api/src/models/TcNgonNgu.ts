import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITcNgonNgu {
  _id: string;
  NgonNguTen: string;
  NgonNguMa: string;
}

const TcNgonNguSchema = new Schema<ITcNgonNgu>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    NgonNguTen: { type: String },
    NgonNguMa: { type: String, default: '' },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const TcNgonNgu = mongoose.model<ITcNgonNgu>('TcNgonNgu', TcNgonNguSchema, 'TcNgonNgus');
export default TcNgonNgu;
