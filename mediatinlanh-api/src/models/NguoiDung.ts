import mongoose, { Schema, CallbackWithoutResultAndOptionalError } from 'mongoose';
import crypto from 'crypto';
import bcrypt from 'bcryptjs';

export interface INguoiDung {
  _id: string;
  TenDangNhap: string;
  MatKhau: string;
  Email: string;
  HoTen: string;
  TrangThai: boolean;
  NgayTao: Date;
}

const NguoiDungSchema = new Schema<INguoiDung>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TenDangNhap: { type: String },
    MatKhau: { type: String },
    Email: { type: String, default: '' },
    HoTen: { type: String, default: '' },
    TrangThai: { type: Boolean, default: true },
    NgayTao: { type: Date, default: Date.now },
  },
  { timestamps: false, versionKey: false, strict: false }
);

NguoiDungSchema.pre('save', async function() {
  if (this.isModified('MatKhau')) {
    this.MatKhau = await bcrypt.hash(this.MatKhau, 10);
  }
});

const NguoiDung = mongoose.model<INguoiDung>('NguoiDung', NguoiDungSchema, 'NguoiDungs');
export default NguoiDung;
