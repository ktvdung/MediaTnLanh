import mongoose, { Schema } from 'mongoose';
import crypto from 'crypto';

export interface ITag {
  _id: string;
  TagTen: string;
}

const TagSchema = new Schema<ITag>(
  {
    _id: { type: String, default: () => crypto.randomUUID() },
    TagTen: { type: String },
  },
  { timestamps: false, versionKey: false, strict: false }
);

const Tag = mongoose.model<ITag>('Tag', TagSchema, 'Tags');
export default Tag;
