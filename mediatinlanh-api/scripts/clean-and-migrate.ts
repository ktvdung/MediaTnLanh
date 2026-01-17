import mongoose from 'mongoose';
import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

// Table mappings with collection names
const tables = [
  { mysql: 'API', mongo: 'Apis' },
  { mysql: 'NgonNgu', mongo: 'NgonNgus' },
  { mysql: 'CauHinh', mongo: 'CauHinhs' },
  { mysql: 'BanMau', mongo: 'BanMaus' },
  { mysql: 'TienTo', mongo: 'TienTos' },
  { mysql: 'TC_TheLoai', mongo: 'TcTheLoais' },
  { mysql: 'TC_NgonNgu', mongo: 'TcNgonNgus' },
  { mysql: 'TC_TheLoai_NgonNgu', mongo: 'TcTheLoaiNgonNgus' },
  { mysql: 'TC_ThanhCa', mongo: 'TcThanhCas' },
  { mysql: 'TC_Loi', mongo: 'TcLois' },
  { mysql: 'KT_BanDich', mongo: 'KtBanDichs' },
  { mysql: 'KT_Sach', mongo: 'KtSaches' },
  { mysql: 'KT_Cau', mongo: 'KtCaus' },
  { mysql: 'KA_TheLoai', mongo: 'KaTheLoais' },
  { mysql: 'KA_BaiHat', mongo: 'KaBaiHats' },
  { mysql: 'DKT_Loai', mongo: 'DktLoais' },
  { mysql: 'DKT_ChuDe', mongo: 'DktChuDes' },
  { mysql: 'DKT_Loai_NgonNgu', mongo: 'DktLoaiNgonNgus' },
  { mysql: 'DKT_ChuDe_NgonNgu', mongo: 'DktChuDeNgonNgus' },
  { mysql: 'DKT_CauHoi', mongo: 'DktCauHois' },
  { mysql: 'NguoiDung', mongo: 'NguoiDungs' },
  { mysql: 'Tag', mongo: 'Tags' },
  { mysql: 'DoiTuong', mongo: 'DoiTuongs', idField: 'IDDoiTuong' },
  { mysql: 'TepTin_TheLoai', mongo: 'TepTinTheLoais' },
  { mysql: 'TepTin', mongo: 'TepTins' },
  { mysql: 'HuongDan', mongo: 'HuongDans' },
  { mysql: 'NhatKy', mongo: 'NhatKys' },
  { mysql: 'CapNhat', mongo: 'CapNhats' },
  { mysql: 'Files_Servers', mongo: 'FilesServers', idField: 'IDFilesServer' },
];

async function migrate() {
  console.log('🚀 Clean Migration from MySQL to MongoDB\n');

  // Connect to MongoDB
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('✅ MongoDB connected');

  // Connect to MySQL
  const mysqlConn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });
  console.log('✅ MySQL connected\n');

  let totalMigrated = 0;

  for (const { mysql: tableName, mongo: collectionName, idField } of tables) {
    console.log(`📦 ${tableName} -> ${collectionName}`);
    
    try {
      // Check if table exists
      const [rows] = await mysqlConn.query(`SELECT * FROM ${tableName}`);
      const data = rows as any[];
      
      if (data.length === 0) {
        console.log(`   Skipped (empty)\n`);
        continue;
      }

      // Drop and recreate collection
      try {
        await mongoose.connection.db!.collection(collectionName).drop();
      } catch (e) {
        // Collection may not exist
      }

      // Transform data - use unique _id based on row index if duplicates exist
      const seenIds = new Set<string>();
      const docs = data.map((row, index) => {
        const doc: any = { ...row };
        
        // Get ID from appropriate field
        let id: string;
        if (idField && row[idField] !== undefined) {
          id = String(row[idField]);
          delete doc[idField];
        } else if (row.ID !== undefined) {
          id = String(row.ID);
          delete doc.ID;
        } else if (row.id !== undefined) {
          id = String(row.id);
          delete doc.id;
        } else {
          id = `auto_${index}`;
        }

        // Handle duplicates by appending index
        if (seenIds.has(id)) {
          id = `${id}_${index}`;
        }
        seenIds.add(id);
        doc._id = id;
        
        return doc;
      });

      // Insert in batches
      const batchSize = 5000;
      let inserted = 0;
      
      for (let i = 0; i < docs.length; i += batchSize) {
        const batch = docs.slice(i, i + batchSize);
        await mongoose.connection.db!.collection(collectionName).insertMany(batch, { ordered: false });
        inserted += batch.length;
      }

      console.log(`   ✅ ${inserted.toLocaleString()} records\n`);
      totalMigrated += inserted;
    } catch (err: any) {
      console.log(`   ❌ Error: ${err.message}\n`);
    }
  }

  // Summary
  console.log('='.repeat(50));
  console.log(`TOTAL MIGRATED: ${totalMigrated.toLocaleString()} records`);
  console.log('='.repeat(50));

  await mysqlConn.end();
  await mongoose.connection.close();
}

migrate();
