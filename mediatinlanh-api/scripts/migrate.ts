import mongoose from 'mongoose';
import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

// Import all MongoDB models
import Api from '../src/models/Api';
import NgonNgu from '../src/models/NgonNgu';
import CauHinh from '../src/models/CauHinh';
import BanMau from '../src/models/BanMau';
import TienTo from '../src/models/TienTo';
import TcTheLoai from '../src/models/TcTheLoai';
import TcNgonNgu from '../src/models/TcNgonNgu';
import TcTheLoaiNgonNgu from '../src/models/TcTheLoaiNgonNgu';
import TcThanhCa from '../src/models/TcThanhCa';
import TcLoi from '../src/models/TcLoi';
import KtBanDich from '../src/models/KtBanDich';
import KtSach from '../src/models/KtSach';
import KtCau from '../src/models/KtCau';
import KaTheLoai from '../src/models/KaTheLoai';
import KaBaiHat from '../src/models/KaBaiHat';
import DktLoai from '../src/models/DktLoai';
import DktChuDe from '../src/models/DktChuDe';
import DktLoaiNgonNgu from '../src/models/DktLoaiNgonNgu';
import DktChuDeNgonNgu from '../src/models/DktChuDeNgonNgu';
import DktCauHoi from '../src/models/DktCauHoi';
import NguoiDung from '../src/models/NguoiDung';
import Tag from '../src/models/Tag';
import DoiTuong from '../src/models/DoiTuong';
import TepTinTheLoai from '../src/models/TepTinTheLoai';
import TepTin from '../src/models/TepTin';
import HuongDan from '../src/models/HuongDan';
import NhatKy from '../src/models/NhatKy';
import CapNhat from '../src/models/CapNhat';
import FilesServer from '../src/models/FilesServer';

interface MigrationConfig {
  mysql: {
    host: string;
    port: number;
    user: string;
    password: string;
    database: string;
  };
  mongodb: string;
}

const config: MigrationConfig = {
  mysql: {
    host: process.env.MYSQL_HOST || 'localhost',
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER || 'root',
    password: process.env.MYSQL_PASSWORD || '',
    database: process.env.MYSQL_DATABASE || 'mediatinlanh',
  },
  mongodb: process.env.MONGODB_URI || '',
};

// MySQL table to MongoDB Model mapping (actual table names from database)
// idField specifies which field to use as _id in MongoDB
const tableModelMap: Array<{ table: string; model: mongoose.Model<any>; name: string; idField?: string }> = [
  { table: 'API', model: Api, name: 'Api' },
  { table: 'NgonNgu', model: NgonNgu, name: 'NgonNgu' },
  { table: 'CauHinh', model: CauHinh, name: 'CauHinh' },
  { table: 'BanMau', model: BanMau, name: 'BanMau' },
  { table: 'TienTo', model: TienTo, name: 'TienTo' },
  { table: 'TC_TheLoai', model: TcTheLoai, name: 'TcTheLoai' },
  { table: 'TC_NgonNgu', model: TcNgonNgu, name: 'TcNgonNgu' },
  { table: 'TC_TheLoai_NgonNgu', model: TcTheLoaiNgonNgu, name: 'TcTheLoaiNgonNgu' },
  { table: 'TC_ThanhCa', model: TcThanhCa, name: 'TcThanhCa' },
  { table: 'TC_Loi', model: TcLoi, name: 'TcLoi' },
  { table: 'KT_BanDich', model: KtBanDich, name: 'KtBanDich' },
  { table: 'KT_Sach', model: KtSach, name: 'KtSach' },
  { table: 'KT_Cau', model: KtCau, name: 'KtCau' },
  { table: 'KA_TheLoai', model: KaTheLoai, name: 'KaTheLoai' },
  { table: 'KA_BaiHat', model: KaBaiHat, name: 'KaBaiHat' },
  { table: 'DKT_Loai', model: DktLoai, name: 'DktLoai' },
  { table: 'DKT_ChuDe', model: DktChuDe, name: 'DktChuDe' },
  { table: 'DKT_Loai_NgonNgu', model: DktLoaiNgonNgu, name: 'DktLoaiNgonNgu' },
  { table: 'DKT_ChuDe_NgonNgu', model: DktChuDeNgonNgu, name: 'DktChuDeNgonNgu' },
  { table: 'DKT_CauHoi', model: DktCauHoi, name: 'DktCauHoi' },
  { table: 'NguoiDung', model: NguoiDung, name: 'NguoiDung' },
  { table: 'Tag', model: Tag, name: 'Tag' },
  { table: 'DoiTuong', model: DoiTuong, name: 'DoiTuong', idField: 'IDDoiTuong' },
  { table: 'TepTin_TheLoai', model: TepTinTheLoai, name: 'TepTinTheLoai' },
  { table: 'TepTin', model: TepTin, name: 'TepTin' },
  { table: 'HuongDan', model: HuongDan, name: 'HuongDan' },
  { table: 'NhatKy', model: NhatKy, name: 'NhatKy' },
  { table: 'CapNhat', model: CapNhat, name: 'CapNhat' },
  { table: 'Files_Servers', model: FilesServer, name: 'FilesServer', idField: 'IDFilesServer' },
];

async function getTableColumns(conn: mysql.Connection, tableName: string): Promise<string[]> {
  try {
    const [rows] = await conn.query(`SHOW COLUMNS FROM ${tableName}`);
    return (rows as any[]).map(r => r.Field);
  } catch {
    return [];
  }
}

async function tableExists(conn: mysql.Connection, tableName: string): Promise<boolean> {
  try {
    await conn.query(`SELECT 1 FROM ${tableName} LIMIT 1`);
    return true;
  } catch {
    return false;
  }
}

async function migrateTable(
  mysqlConn: mysql.Connection,
  tableName: string,
  model: mongoose.Model<any>,
  displayName: string,
  idField?: string
): Promise<{ migrated: number; errors: number; skipped: boolean }> {
  console.log(`\n📦 Migrating: ${displayName} (${tableName})`);
  
  // Check if table exists
  const exists = await tableExists(mysqlConn, tableName);
  if (!exists) {
    console.log(`   ⚠️ Table ${tableName} does not exist, skipping...`);
    return { migrated: 0, errors: 0, skipped: true };
  }

  let migrated = 0;
  let errors = 0;

  try {
    const [rows] = await mysqlConn.query(`SELECT * FROM ${tableName}`);
    const data = rows as any[];
    
    console.log(`   Found ${data.length} records`);

    if (data.length === 0) {
      return { migrated: 0, errors: 0, skipped: false };
    }

    // Clear existing data in MongoDB
    await model.deleteMany({});

    // Batch insert for better performance
    const batchSize = 1000;
    for (let i = 0; i < data.length; i += batchSize) {
      const batch = data.slice(i, i + batchSize);
      const docs = batch.map(row => {
        const doc: any = { ...row };
        // Handle ID field - preserve original ID as _id
        // Use custom idField if specified, otherwise try common patterns
        if (idField && row[idField] !== undefined) {
          doc._id = row[idField];
          delete doc[idField];
        } else if (row.id !== undefined) {
          doc._id = row.id;
          delete doc.id;
        } else if (row.ID !== undefined) {
          doc._id = row.ID;
          delete doc.ID;
        }
        return doc;
      });

      try {
        await model.insertMany(docs, { ordered: false });
        migrated += docs.length;
      } catch (err: any) {
        // Handle partial success in insertMany
        if (err.insertedDocs) {
          migrated += err.insertedDocs.length;
        }
        errors += batch.length - (err.insertedDocs?.length || 0);
        console.error(`   ⚠️ Batch error:`, err.message?.substring(0, 100));
      }

      // Progress indicator
      if (data.length > batchSize) {
        const progress = Math.min(100, Math.round((i + batchSize) / data.length * 100));
        process.stdout.write(`\r   Progress: ${progress}%`);
      }
    }

    if (data.length > batchSize) {
      console.log(''); // New line after progress
    }

    console.log(`   ✅ Migrated: ${migrated}, Errors: ${errors}`);
  } catch (err: any) {
    console.error(`   ❌ Error:`, err.message);
    errors++;
  }

  return { migrated, errors, skipped: false };
}

async function validateMigration(
  mysqlConn: mysql.Connection,
  tableName: string,
  model: mongoose.Model<any>,
  displayName: string
): Promise<{ valid: boolean; mysqlCount: number; mongoCount: number }> {
  try {
    const exists = await tableExists(mysqlConn, tableName);
    if (!exists) {
      return { valid: true, mysqlCount: 0, mongoCount: 0 };
    }

    const [mysqlRows] = await mysqlConn.query(`SELECT COUNT(*) as count FROM ${tableName}`);
    const mysqlCount = (mysqlRows as any[])[0].count;
    
    const mongoCount = await model.countDocuments();
    
    const isValid = mysqlCount === mongoCount;
    const status = isValid ? '✅' : '❌';
    console.log(`   ${displayName}: MySQL=${mysqlCount}, MongoDB=${mongoCount} ${status}`);
    
    return { valid: isValid, mysqlCount, mongoCount };
  } catch (err: any) {
    console.error(`   Error validating ${displayName}:`, err.message);
    return { valid: false, mysqlCount: 0, mongoCount: 0 };
  }
}

async function migrate() {
  console.log('🚀 Starting migration from MySQL to MongoDB...\n');
  console.log('MySQL Config:', {
    host: config.mysql.host,
    port: config.mysql.port,
    database: config.mysql.database,
    user: config.mysql.user,
  });
  console.log('MongoDB:', config.mongodb.substring(0, 50) + '...\n');
  
  let mysqlConn: mysql.Connection | null = null;
  
  try {
    // Connect to MySQL
    console.log('📡 Connecting to MySQL...');
    mysqlConn = await mysql.createConnection({
      host: config.mysql.host,
      port: config.mysql.port,
      user: config.mysql.user,
      password: config.mysql.password,
      database: config.mysql.database,
    });
    console.log('✅ MySQL connected\n');

    // Connect to MongoDB
    console.log('📡 Connecting to MongoDB...');
    await mongoose.connect(config.mongodb);
    console.log('✅ MongoDB connected\n');

    // Migration stats
    let totalMigrated = 0;
    let totalErrors = 0;
    let skippedTables = 0;

    // Migrate each table
    console.log('=' .repeat(50));
    console.log('📦 MIGRATING TABLES');
    console.log('='.repeat(50));

    for (const { table, model, name, idField } of tableModelMap) {
      const result = await migrateTable(mysqlConn, table, model, name, idField);
      totalMigrated += result.migrated;
      totalErrors += result.errors;
      if (result.skipped) skippedTables++;
    }

    // Validation
    console.log('\n' + '='.repeat(50));
    console.log('📊 VALIDATING MIGRATION');
    console.log('='.repeat(50));
    
    let validationPassed = true;
    let totalMysqlRecords = 0;
    let totalMongoRecords = 0;

    for (const { table, model, name } of tableModelMap) {
      const result = await validateMigration(mysqlConn, table, model, name);
      if (!result.valid) validationPassed = false;
      totalMysqlRecords += result.mysqlCount;
      totalMongoRecords += result.mongoCount;
    }

    // Summary
    console.log('\n' + '='.repeat(50));
    console.log('📈 MIGRATION SUMMARY');
    console.log('='.repeat(50));
    console.log(`Tables processed: ${tableModelMap.length}`);
    console.log(`Tables skipped: ${skippedTables}`);
    console.log(`Total MySQL records: ${totalMysqlRecords}`);
    console.log(`Total MongoDB records: ${totalMongoRecords}`);
    console.log(`Records migrated: ${totalMigrated}`);
    console.log(`Errors: ${totalErrors}`);
    console.log(`Validation: ${validationPassed ? '✅ PASSED' : '❌ FAILED'}`);
    console.log('='.repeat(50));

  } catch (err: any) {
    console.error('❌ Migration failed:', err.message);
    console.error(err);
  } finally {
    if (mysqlConn) await mysqlConn.end();
    await mongoose.connection.close();
    console.log('\n👋 Connections closed');
  }
}

// Run migration
migrate();
