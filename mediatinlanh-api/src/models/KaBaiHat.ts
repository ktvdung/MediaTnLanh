import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IKaBaiHat {
  _id: string;
  BaiHatTen: string;
  ID_KA_TheLoai: string;
  TacGia: string;
  NamSangTac: number;
}

const KaBaiHatSchema = new Schema<IKaBaiHat>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    BaiHatTen: { type: String },
    ID_KA_TheLoai: { type: String, default: '' },
    TacGia: { type: String, default: '' },
    NamSangTac: { type: Number, default: 0 },
  },
  { timestamps: false, versionKey: false, strict: false }
);

KaBaiHatSchema.index({ ID_KA_TheLoai: 1 });

const KaBaiHat = mongoose.model<IKaBaiHat>('KaBaiHat', KaBaiHatSchema, 'KaBaiHats');
export default KaBaiHat;
