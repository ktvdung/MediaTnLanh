import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITepTin {
  _id: string;
  IDDoiTuong?: string;
  TenTepTin?: string;
  TenHienThi?: string;
  MD5?: string;
  DiaChiMayChu?: string;
  UrlTep?: string;
  NguoiTaiLen?: string;
  IPTaiLen?: string;
  GhiChu?: string;
  TrangThai?: string;
  ThoiGianTaiLen?: Date;
  IDTheLoaiTepTin?: string;
  UrlDaiDien?: string;
  DungLuong?: number;
  LuotTai?: number;
  LuotXem?: number;
}

const TepTinSchema = new Schema<ITepTin>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    IDDoiTuong: { type: String },
    TenTepTin: { type: String },
    TenHienThi: { type: String },
    MD5: { type: String },
    DiaChiMayChu: { type: String },
    UrlTep: { type: String },
    NguoiTaiLen: { type: String },
    IPTaiLen: { type: String },
    GhiChu: { type: String },
    TrangThai: { type: String },
    ThoiGianTaiLen: { type: Date },
    IDTheLoaiTepTin: { type: String },
    UrlDaiDien: { type: String },
    DungLuong: { type: Number },
    LuotTai: { type: Number },
    LuotXem: { type: Number },
  },
  { timestamps: false, versionKey: false, strict: false }
);

TepTinSchema.index({ IDTheLoaiTepTin: 1 });

const TepTin = mongoose.model<ITepTin>('TepTin', TepTinSchema, 'TepTins');
export default TepTin;
