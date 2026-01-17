import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITcTheLoai {
  _id: string;
  ThanhCaTheLoaiTen?: string;
}

const TcTheLoaiSchema = new Schema<ITcTheLoai>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ThanhCaTheLoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const TcTheLoai = mongoose.model<ITcTheLoai>('TcTheLoai', TcTheLoaiSchema, 'TcTheLoais');
export default TcTheLoai;
