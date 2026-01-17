import mongoose from 'mongoose';
import dotenv from 'dotenv';

dotenv.config();

async function verify() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  const collections = await mongoose.connection.db!.listCollections().toArray();
  console.log('='.repeat(50));
  console.log('MONGODB COLLECTIONS SUMMARY');
  console.log('='.repeat(50));
  
  let total = 0;
  for (const col of collections.sort((a, b) => a.name.localeCompare(b.name))) {
    const count = await mongoose.connection.db!.collection(col.name).countDocuments();
    total += count;
    console.log(`${col.name.padEnd(25)} ${count.toLocaleString().padStart(10)}`);
  }
  
  console.log('='.repeat(50));
  console.log(`TOTAL RECORDS:${total.toLocaleString().padStart(21)}`);
  console.log('='.repeat(50));

  await mongoose.connection.close();
}

verify();
