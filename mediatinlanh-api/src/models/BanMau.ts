import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IBanMau {
  _id: string;
  BanMauTen: string;
  BanMauNoiDung: string;
}

const BanMauSchema = new Schema<IBanMau>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    BanMauTen: { type: String },
    BanMauNoiDung: { type: String, default: '' },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const BanMau = mongoose.model<IBanMau>('BanMau', BanMauSchema, 'BanMaus');
export default BanMau;
