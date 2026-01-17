import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDktLoaiNgonNgu {
  _id: string;
  ID_DKT_Loai: string;
  ID_NgonNgu: string;
  LoaiTen: string;
}

const DktLoaiNgonNguSchema = new Schema<IDktLoaiNgonNgu>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ID_DKT_Loai: { type: String },
    ID_NgonNgu: { type: String },
    LoaiTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const DktLoaiNgonNgu = mongoose.model<IDktLoaiNgonNgu>('DktLoaiNgonNgu', DktLoaiNgonNguSchema, 'DktLoaiNgonNgus');
export default DktLoaiNgonNgu;
