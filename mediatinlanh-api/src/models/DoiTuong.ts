import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDoiTuong {
  _id: string;
  DoiTuongTen: string;
  DoiTuongMoTa: string;
  Tags: string[];
}

const DoiTuongSchema = new Schema<IDoiTuong>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    DoiTuongTen: { type: String },
    DoiTuongMoTa: { type: String, default: '' },
    Tags: [{ type: String }],
  },
  { timestamps: false, versionKey: false, strict: false }
);

const DoiTuong = mongoose.model<IDoiTuong>('DoiTuong', DoiTuongSchema, 'DoiTuongs');
export default DoiTuong;
