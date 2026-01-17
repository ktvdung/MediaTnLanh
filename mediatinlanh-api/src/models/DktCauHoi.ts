import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IDktCauHoi {
  _id: string;
  AnswerA?: string;
  AnswerB?: string;
  AnswerC?: string;
  AnswerD?: string;
  CorrectIndex?: number;
  Question?: string;
  QuestionIndex?: number;
  ID_DKT_ChuDe?: string;
  MaCode?: string;
  ID_DKT_Loai?: string;
  Answer?: string;
  BeginIndex?: number;
  MainIndex?: number;
  RowIndex?: number;
  Correct?: string;
  QuestionA?: string;
  QuestionB?: string;
  QuestionC?: string;
  ID_KT_BanDich?: string;
  IDNgonNgu?: string;
  IDCauHoiGoc?: string;
  IDDoiTuong?: string;
}

const DktCauHoiSchema = new Schema<IDktCauHoi>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    AnswerA: { type: String },
    AnswerB: { type: String },
    AnswerC: { type: String },
    AnswerD: { type: String },
    CorrectIndex: { type: Number },
    Question: { type: String },
    QuestionIndex: { type: Number },
    ID_DKT_ChuDe: { type: String },
    MaCode: { type: String },
    ID_DKT_Loai: { type: String },
    Answer: { type: String },
    BeginIndex: { type: Number },
    MainIndex: { type: Number },
    RowIndex: { type: Number },
    Correct: { type: String },
    QuestionA: { type: String },
    QuestionB: { type: String },
    QuestionC: { type: String },
    ID_KT_BanDich: { type: String },
    IDNgonNgu: { type: String },
    IDCauHoiGoc: { type: String },
    IDDoiTuong: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

DktCauHoiSchema.index({ ID_DKT_ChuDe: 1 });

const DktCauHoi = mongoose.model<IDktCauHoi>('DktCauHoi', DktCauHoiSchema, 'DktCauHois');
export default DktCauHoi;
