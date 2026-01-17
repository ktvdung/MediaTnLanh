import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITcLoi {
  _id: string;
  CauSo?: number;
  DiepKhuc?: string;
  NoiDung?: string;
  ID_TC_ThanhCa?: string;
}

const TcLoiSchema = new Schema<ITcLoi>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    CauSo: { type: Number },
    DiepKhuc: { type: String },
    NoiDung: { type: String },
    ID_TC_ThanhCa: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

TcLoiSchema.index({ ID_TC_ThanhCa: 1 });

const TcLoi = mongoose.model<ITcLoi>('TcLoi', TcLoiSchema, 'TcLois');
export default TcLoi;
