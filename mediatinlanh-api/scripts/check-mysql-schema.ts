import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

const tables = [
  'TC_NgonNgu',
  'TC_TheLoai_NgonNgu', 
  'KA_TheLoai',
  'DKT_Loai',
  'DKT_ChuDe',
  'NguoiDung',
  'DoiTuong',
  'TepTin_TheLoai',
  'TepTin',
  'HuongDan',
  'Files_Servers'
];

async function check() {
  const conn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });

  for (const table of tables) {
    console.log(`\n=== ${table} ===`);
    try {
      const [columns] = await conn.query(`SHOW COLUMNS FROM ${table}`);
      console.log('Columns:', (columns as any[]).map(c => c.Field).join(', '));
      
      const [rows] = await conn.query(`SELECT * FROM ${table} LIMIT 1`);
      if ((rows as any[]).length > 0) {
        console.log('Sample:', JSON.stringify((rows as any[])[0], null, 2));
      } else {
        console.log('No data');
      }
    } catch (err: any) {
      console.log('Error:', err.message);
    }
  }

  await conn.end();
}

check();
