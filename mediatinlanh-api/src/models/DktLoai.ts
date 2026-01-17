import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDktLoai {
  _id: string;
  LoaiTen: string;
}

const DktLoaiSchema = new Schema<IDktLoai>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    LoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const DktLoai = mongoose.model<IDktLoai>('DktLoai', DktLoaiSchema, 'DktLoais');
export default DktLoai;
