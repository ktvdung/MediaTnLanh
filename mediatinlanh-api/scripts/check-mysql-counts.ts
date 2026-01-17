import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

const tables = [
  'TC_NgonNgu',
  'TC_TheLoai_NgonNgu', 
  'KA_TheLoai',
  'KA_BaiHat',
  'DKT_Loai',
  'DKT_ChuDe',
  'DKT_Loai_NgonNgu',
  'DKT_ChuDe_NgonNgu',
  'NguoiDung',
  'Tag',
  'DoiTuong',
  'TepTin_TheLoai',
  'TepTin',
  'HuongDan',
  'CapNhat',
  'Files_Servers',
  'TienTo'
];

async function check() {
  const conn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });

  console.log('MySQL table counts:\n');

  for (const table of tables) {
    try {
      const [rows] = await conn.query(`SELECT COUNT(*) as count FROM ${table}`);
      const count = (rows as any[])[0].count;
      console.log(`${table}: ${count}`);
    } catch (err: any) {
      console.log(`${table}: ERROR - ${err.message}`);
    }
  }

  await conn.end();
}

check();
