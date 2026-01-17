import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITienTo {
  _id: string;
  TienToTen: string;
  TienToGiaTri: string;
}

const TienToSchema = new Schema<ITienTo>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TienToTen: { type: String },
    TienToGiaTri: { type: String, default: '' },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const TienTo = mongoose.model<ITienTo>('TienTo', TienToSchema, 'TienTos');
export default TienTo;
