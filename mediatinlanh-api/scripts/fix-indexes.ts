import mongoose from 'mongoose';
import dotenv from 'dotenv';

dotenv.config();

async function fixIndexes() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  const collectionsToFix = [
    'CauHinhs', 'TcNgonNgus', 'TcTheLoaiNgonNgus', 'KaTheLoais',
    'DktLoais', 'DktChuDes', 'NguoiDungs', 'DoiTuongs',
    'TepTinTheLoais', 'TepTins', 'HuongDans', 'FilesServers'
  ];

  for (const colName of collectionsToFix) {
    try {
      const col = mongoose.connection.db!.collection(colName);
      
      // Drop all indexes except _id
      console.log(`Dropping indexes for ${colName}...`);
      await col.dropIndexes();
      
      // Delete all documents
      console.log(`Clearing ${colName}...`);
      await col.deleteMany({});
      
      console.log(`✅ ${colName} ready for re-migration\n`);
    } catch (err: any) {
      console.log(`⚠️ ${colName}: ${err.message}\n`);
    }
  }

  await mongoose.connection.close();
  console.log('Done!');
}

fixIndexes();
