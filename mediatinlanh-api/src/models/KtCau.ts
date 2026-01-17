import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IKtCau {
  _id: string;
  SoThuTu?: number;
  TuaDe?: string;
  NoiDung?: string;
  ID_KT_Sach?: string;
  SoCau?: string;
  Doan?: string;
  IDDoiTuong?: string;
}

const KtCauSchema = new Schema<IKtCau>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    SoThuTu: { type: Number },
    TuaDe: { type: String },
    NoiDung: { type: String },
    ID_KT_Sach: { type: String },
    SoCau: { type: String },
    Doan: { type: String },
    IDDoiTuong: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

KtCauSchema.index({ ID_KT_Sach: 1, Doan: 1 });

const KtCau = mongoose.model<IKtCau>('KtCau', KtCauSchema, 'KtCaus');
export default KtCau;
