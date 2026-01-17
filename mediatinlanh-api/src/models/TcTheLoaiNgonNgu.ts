import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITcTheLoaiNgonNgu {
  _id: string;
  ID_TC_TheLoai: string;
  ID_NgonNgu: string;
  TheLoaiTen: string;
}

const TcTheLoaiNgonNguSchema = new Schema<ITcTheLoaiNgonNgu>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ID_TC_TheLoai: { type: String },
    ID_NgonNgu: { type: String },
    TheLoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const TcTheLoaiNgonNgu = mongoose.model<ITcTheLoaiNgonNgu>('TcTheLoaiNgonNgu', TcTheLoaiNgonNguSchema, 'TcTheLoaiNgonNgus');
export default TcTheLoaiNgonNgu;
