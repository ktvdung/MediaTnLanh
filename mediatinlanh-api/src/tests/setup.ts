import mongoose from 'mongoose';

beforeAll(async () => {
  // Setup before all tests
});

afterAll(async () => {
  // Cleanup after all tests
  await mongoose.connection.close();
});

afterEach(async () => {
  // Cleanup after each test if needed
});
