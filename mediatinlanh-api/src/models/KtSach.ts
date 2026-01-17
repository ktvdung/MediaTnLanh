import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IKtSach {
  _id: string;
  TenSach?: string;
  SoThuTu?: number;
  CuuTanUoc?: string;
  NhomSach?: string;
  TongSoChuong?: number;
  ID_KT_BanDich?: string;
}

const KtSachSchema = new Schema<IKtSach>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TenSach: { type: String },
    SoThuTu: { type: Number },
    CuuTanUoc: { type: String },
    NhomSach: { type: String },
    TongSoChuong: { type: Number },
    ID_KT_BanDich: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

KtSachSchema.index({ ID_KT_BanDich: 1 });

const KtSach = mongoose.model<IKtSach>('KtSach', KtSachSchema, 'KtSaches');
export default KtSach;
