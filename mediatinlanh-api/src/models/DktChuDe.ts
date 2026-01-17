import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDktChuDe {
  _id: string;
  ChuDeTen: string;
}

const DktChuDeSchema = new Schema<IDktChuDe>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ChuDeTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const DktChuDe = mongoose.model<IDktChuDe>('DktChuDe', DktChuDeSchema, 'DktChuDes');
export default DktChuDe;
