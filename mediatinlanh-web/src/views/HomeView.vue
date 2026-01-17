<template>
  <div class="home">
    <h1>MediaTinLanh</h1>
    <p>Chào mừng đến với ứng dụng MediaTinLanh</p>
    <div class="features">
      <router-link to="/thanh-ca" class="feature-card">
        <h2>Thánh Ca</h2>
        <p>Quản lý bài Thánh Ca</p>
      </router-link>
      <router-link to="/kinh-thanh" class="feature-card">
        <h2>Kinh Thánh</h2>
        <p>Đọc và tìm kiếm Kinh Thánh</p>
      </router-link>
      <router-link to="/karaoke" class="feature-card">
        <h2>Karaoke</h2>
        <p>Bài hát Karaoke</p>
      </router-link>
      <router-link to="/quiz" class="feature-card">
        <h2>Đạo Kinh Thánh</h2>
        <p>Câu hỏi trắc nghiệm</p>
      </router-link>
    </div>
  </div>
</template>

<style scoped>
.home {
  text-align: center;
  padding: 2rem;
}

.features {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1rem;
  margin-top: 2rem;
}

.feature-card {
  padding: 1.5rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  text-decoration: none;
  color: inherit;
  transition: box-shadow 0.2s;
}

.feature-card:hover {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.feature-card h2 {
  margin: 0 0 0.5rem;
  color: #333;
}

.feature-card p {
  margin: 0;
  color: #666;
}
</style>
