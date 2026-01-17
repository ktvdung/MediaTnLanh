import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITepTinTheLoai {
  _id: string;
  TheLoaiTen: string;
}

const TepTinTheLoaiSchema = new Schema<ITepTinTheLoai>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TheLoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const TepTinTheLoai = mongoose.model<ITepTinTheLoai>('TepTinTheLoai', TepTinTheLoaiSchema, 'TepTinTheLoais');
export default TepTinTheLoai;
