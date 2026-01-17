import mongoose from 'mongoose';
import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

// Import models
import TcNgonNgu from '../src/models/TcNgonNgu';
import KaTheLoai from '../src/models/KaTheLoai';
import DktLoai from '../src/models/DktLoai';
import DktChuDe from '../src/models/DktChuDe';
import DoiTuong from '../src/models/DoiTuong';
import TepTin from '../src/models/TepTin';
import TepTinTheLoai from '../src/models/TepTinTheLoai';
import HuongDan from '../src/models/HuongDan';
import NguoiDung from '../src/models/NguoiDung';
import FilesServer from '../src/models/FilesServer';
import TcTheLoaiNgonNgu from '../src/models/TcTheLoaiNgonNgu';

async function debug() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  const mysqlConn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });

  // Test with TC_NgonNgu
  console.log('=== Testing TC_NgonNgu ===');
  const [tcNgonNguRows] = await mysqlConn.query('SELECT * FROM TC_NgonNgu');
  console.log('MySQL data:', tcNgonNguRows);
  
  // Clear and insert
  await TcNgonNgu.deleteMany({});
  const tcNgonNguDocs = (tcNgonNguRows as any[]).map(row => {
    const doc: any = { ...row };
    if (row.ID !== undefined) {
      doc._id = String(row.ID);
      delete doc.ID;
    }
    return doc;
  });
  console.log('Docs to insert:', tcNgonNguDocs);
  
  try {
    const result = await TcNgonNgu.insertMany(tcNgonNguDocs);
    console.log('Insert result:', result.length);
  } catch (err: any) {
    console.log('Insert error:', err.message);
  }
  
  // Verify
  const count = await TcNgonNgu.countDocuments();
  console.log('MongoDB count after insert:', count);
  
  const sample = await TcNgonNgu.findOne();
  console.log('Sample from MongoDB:', sample);

  // Check collection name
  console.log('\nCollection name:', TcNgonNgu.collection.name);

  // List all collections
  const collections = await mongoose.connection.db!.listCollections().toArray();
  console.log('\nAll collections:', collections.map(c => c.name));

  await mysqlConn.end();
  await mongoose.connection.close();
}

debug();
