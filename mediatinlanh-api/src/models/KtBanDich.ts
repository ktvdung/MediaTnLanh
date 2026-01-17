import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IKtBanDich {
  _id: string;
  Ma?: string;
  KT_BanDichTen?: string;
  TacGia?: string;
  NhaXuatBan?: string;
  NgonNgu?: string;
  BanQuyen?: string;
  Audio?: string;
  GioiThieu?: string;
  DoDaiCauKinhThanh?: string;
  DoDaiCauKinhThanhSlide?: number;
  IDNgonNgu?: string;
}

const KtBanDichSchema = new Schema<IKtBanDich>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    Ma: { type: String },
    KT_BanDichTen: { type: String },
    TacGia: { type: String },
    NhaXuatBan: { type: String },
    NgonNgu: { type: String },
    BanQuyen: { type: String },
    Audio: { type: String },
    GioiThieu: { type: String },
    DoDaiCauKinhThanh: { type: String },
    DoDaiCauKinhThanhSlide: { type: Number },
    IDNgonNgu: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const KtBanDich = mongoose.model<IKtBanDich>('KtBanDich', KtBanDichSchema, 'KtBanDichs');
export default KtBanDich;
