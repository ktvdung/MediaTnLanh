import mysql from 'mysql2/promise';
import dotenv from 'dotenv';

dotenv.config();

async function showTables() {
  const conn = await mysql.createConnection({
    host: process.env.MYSQL_HOST,
    port: parseInt(process.env.MYSQL_PORT || '3306'),
    user: process.env.MYSQL_USER,
    password: process.env.MYSQL_PASSWORD,
    database: process.env.MYSQL_DATABASE,
  });
  
  const [rows] = await conn.query('SHOW TABLES');
  console.log('Tables in database:');
  (rows as any[]).forEach((r: any) => console.log('  -', Object.values(r)[0]));
  
  await conn.end();
}

showTables();
