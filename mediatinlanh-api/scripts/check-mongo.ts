import mongoose from 'mongoose';
import dotenv from 'dotenv';

dotenv.config();

// Import models
import Api from '../src/models/Api';
import NgonNgu from '../src/models/NgonNgu';
import TcThanhCa from '../src/models/TcThanhCa';
import TcLoi from '../src/models/TcLoi';
import KtCau from '../src/models/KtCau';
import KtSach from '../src/models/KtSach';
import KtBanDich from '../src/models/KtBanDich';
import DktCauHoi from '../src/models/DktCauHoi';
import NhatKy from '../src/models/NhatKy';

async function checkMongo() {
  console.log('Connecting to MongoDB...');
  await mongoose.connect(process.env.MONGODB_URI || '');
  console.log('Connected!\n');

  const models = [
    { name: 'Api', model: Api },
    { name: 'NgonNgu', model: NgonNgu },
    { name: 'TcThanhCa', model: TcThanhCa },
    { name: 'TcLoi', model: TcLoi },
    { name: 'KtCau', model: KtCau },
    { name: 'KtSach', model: KtSach },
    { name: 'KtBanDich', model: KtBanDich },
    { name: 'DktCauHoi', model: DktCauHoi },
    { name: 'NhatKy', model: NhatKy },
  ];

  console.log('Document counts in MongoDB:');
  for (const { name, model } of models) {
    const count = await model.countDocuments();
    console.log(`  ${name}: ${count}`);
  }

  // Show sample data
  console.log('\nSample TcThanhCa:');
  const sample = await TcThanhCa.findOne().lean();
  console.log(sample);

  await mongoose.connection.close();
}

checkMongo();
