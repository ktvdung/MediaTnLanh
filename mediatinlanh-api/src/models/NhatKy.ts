import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface INhatKy {
  _id: string;
  HanhDong: string;
  DoiTuong: string;
  ID_NguoiDung: string;
  IP: string;
  NgayTao: Date;
}

const NhatKySchema = new Schema<INhatKy>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    HanhDong: { type: String },
    DoiTuong: { type: String, default: '' },
    ID_NguoiDung: { type: String, default: '' },
    IP: { type: String, default: '' },
    NgayTao: { type: Date, default: Date.now },
  },
  { timestamps: false, versionKey: false, strict: false }
);

NhatKySchema.index({ NgayTao: -1 });

const NhatKy = mongoose.model<INhatKy>('NhatKy', NhatKySchema, 'NhatKys');
export default NhatKy;
