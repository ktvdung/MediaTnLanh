import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IKaTheLoai {
  _id: string;
  TheLoaiTen: string;
}

const KaTheLoaiSchema = new Schema<IKaTheLoai>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TheLoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const KaTheLoai = mongoose.model<IKaTheLoai>('KaTheLoai', KaTheLoaiSchema, 'KaTheLoais');
export default KaTheLoai;
