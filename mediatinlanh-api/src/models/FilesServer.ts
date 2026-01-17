import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface IFilesServer {
  _id: string;
  ServerTen: string;
  ServerUrl: string;
  TrangThai: boolean;
}

const FilesServerSchema = new Schema<IFilesServer>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    ServerTen: { type: String },
    ServerUrl: { type: String, default: '' },
    TrangThai: { type: Boolean, default: true },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const FilesServer = mongoose.model<IFilesServer>('FilesServer', FilesServerSchema, 'FilesServers');
export default FilesServer;
