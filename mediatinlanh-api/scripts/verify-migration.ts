import mongoose from 'mongoose';
import dotenv from 'dotenv';

dotenv.config();

async function verify() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  const collections = await mongoose.connection.db!.listCollections().toArray();
  console.log('Collections in MongoDB:');
  
  for (const col of collections) {
    const count = await mongoose.connection.db!.collection(col.name).countDocuments();
    console.log(`  ${col.name}: ${count}`);
  }

  // Sample data from key collections
  console.log('\n--- Sample TcLoi ---');
  const tcLoi = await mongoose.connection.db!.collection('TcLois').findOne();
  console.log(tcLoi);

  console.log('\n--- Sample KtCau ---');
  const ktCau = await mongoose.connection.db!.collection('KtCaus').findOne();
  console.log(ktCau);

  console.log('\n--- Sample DktCauHoi ---');
  const dktCauHoi = await mongoose.connection.db!.collection('DktCauHois').findOne();
  console.log(dktCauHoi);

  await mongoose.connection.close();
}

verify();
