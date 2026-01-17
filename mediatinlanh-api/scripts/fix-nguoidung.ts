import mongoose from 'mongoose';
import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

async function fix() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  // Drop the problematic index
  try {
    await mongoose.connection.db!.collection('NguoiDungs').dropIndex('TenDangNhap_1');
    console.log('Dropped TenDangNhap_1 index');
  } catch (err: any) {
    console.log('Index may not exist:', err.message);
  }

  // Clear collection
  await mongoose.connection.db!.collection('NguoiDungs').deleteMany({});
  console.log('Cleared NguoiDungs collection');

  // Connect to MySQL and migrate
  const mysqlConn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });

  const [rows] = await mysqlConn.query('SELECT * FROM NguoiDung');
  console.log('MySQL NguoiDung records:', (rows as any[]).length);
  console.log('Data:', rows);

  // Insert directly without model validation
  const docs = (rows as any[]).map(row => {
    const doc: any = { ...row };
    if (row.ID !== undefined) {
      doc._id = String(row.ID);
      delete doc.ID;
    }
    return doc;
  });

  console.log('Docs to insert:', docs);

  const result = await mongoose.connection.db!.collection('NguoiDungs').insertMany(docs);
  console.log('Inserted:', result.insertedCount);

  // Verify
  const count = await mongoose.connection.db!.collection('NguoiDungs').countDocuments();
  console.log('Final count:', count);

  await mysqlConn.end();
  await mongoose.connection.close();
}

fix();
