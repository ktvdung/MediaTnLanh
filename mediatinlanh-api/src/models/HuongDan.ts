import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IHuongDan {
  _id: string;
  TieuDe?: string;
  NoiDung?: string;
  ThuTu?: number;
  NgayTao?: Date;
  IPTao?: string;
}

const HuongDanSchema = new Schema<IHuongDan>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TieuDe: { type: String },
    NoiDung: { type: String, default: '' },
    ThuTu: { type: Number },
    NgayTao: { type: Date },
    IPTao: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const HuongDan = mongoose.model<IHuongDan>('HuongDan', HuongDanSchema, 'HuongDans');
export default HuongDan;
