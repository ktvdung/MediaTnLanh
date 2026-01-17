import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITcThanhCa {
  _id: string;
  So: number;
  ThanhCaTen: string;
  ID_TC_TheLoai: string;
  ID_TC_NgonNgu: string;
  Loi: string;
  SoanNhac: string;
  VietLoi: string;
  NgayTao: Date;
  NgaySua: Date;
}

const TcThanhCaSchema = new Schema<ITcThanhCa>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    So: { type: Number },
    ThanhCaTen: { type: String },
    ID_TC_TheLoai: { type: String },
    ID_TC_NgonNgu: { type: String, default: '' },
    Loi: { type: String, default: '' },
    SoanNhac: { type: String, default: '' },
    VietLoi: { type: String, default: '' },
    NgayTao: { type: Date, default: Date.now },
    NgaySua: { type: Date, default: Date.now },
  },
  { timestamps: false, versionKey: false, strict: false }
);

TcThanhCaSchema.index({ ID_TC_TheLoai: 1 });
TcThanhCaSchema.index({ ID_TC_NgonNgu: 1 });

const TcThanhCa = mongoose.model<ITcThanhCa>('TcThanhCa', TcThanhCaSchema, 'TcThanhCas');
export default TcThanhCa;
