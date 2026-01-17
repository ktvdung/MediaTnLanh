import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface INgonNgu {
  _id: string;
  NgonNguTen: string;
  NgonNguMa: string;
}

const NgonNguSchema = new Schema<INgonNgu>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    NgonNguTen: { type: String },
    NgonNguMa: { type: String, default: '' },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const NgonNgu = mongoose.model<INgonNgu>('NgonNgu', NgonNguSchema, 'NgonNgus');
export default NgonNgu;
