import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDktChuDeNgonNgu {
  _id: string;
  ID_DKT_ChuDe: string;
  ID_NgonNgu: string;
  ChuDeTen: string;
}

const DktChuDeNgonNguSchema = new Schema<IDktChuDeNgonNgu>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ID_DKT_ChuDe: { type: String },
    ID_NgonNgu: { type: String },
    ChuDeTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const DktChuDeNgonNgu = mongoose.model<IDktChuDeNgonNgu>('DktChuDeNgonNgu', DktChuDeNgonNguSchema, 'DktChuDeNgonNgus');
export default DktChuDeNgonNgu;
