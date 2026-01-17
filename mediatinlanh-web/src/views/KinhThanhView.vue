<template>
  <div class="kinh-thanh">
    <h1>Kinh Thánh</h1>
    
    <ErrorAlert :message="store.error" dismissible @dismiss="store.error = null" />
    
    <div class="filters">
      <select v-model="selectedBanDich" @change="handleFilter" class="filter-select">
        <option value="">Chọn bản dịch</option>
        <option v-for="bd in store.banDichs" :key="bd._id" :value="bd._id">{{ bd.TenBanDich }}</option>
      </select>
      <select v-model="selectedSach" @change="handleSachChange" class="filter-select">
        <option value="">Chọn sách</option>
        <option v-for="s in store.sachs" :key="s._id" :value="s._id">{{ s.TenSach }}</option>
      </select>
      <select v-model="selectedChuong" @change="handleFilter" class="filter-select" :disabled="!selectedSach">
        <option value="">Chọn chương</option>
        <option v-for="c in chuongOptions" :key="c" :value="c">Chương {{ c }}</option>
      </select>
    </div>

    <SearchBox v-model="searchQuery" placeholder="Tìm kiếm câu Kinh Thánh..." @search="handleSearch" />

    <LoadingSpinner v-if="store.loading" message="Đang tải..." />
    
    <div v-else-if="store.caus.length === 0" class="empty-state">
      <p>Chọn bản dịch, sách và chương để xem nội dung</p>
    </div>
    
    <div v-else class="verses">
      <div v-for="cau in store.caus" :key="cau._id" class="verse">
        <span class="verse-number">{{ cau.Cau }}</span>
        <span class="verse-text">{{ cau.NoiDung }}</span>
      </div>
    </div>

    <Pagination :currentPage="store.page" :totalPages="store.totalPages" @change="handlePageChange" />
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useKinhThanhStore } from '../stores/kinhThanh';
import ErrorAlert from '../components/ErrorAlert.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import SearchBox from '../components/SearchBox.vue';
import Pagination from '../components/Pagination.vue';

const store = useKinhThanhStore();
const searchQuery = ref('');
const selectedBanDich = ref('');
const selectedSach = ref('');
const selectedChuong = ref<number | ''>('');

const chuongOptions = computed(() => {
  const sach = store.sachs.find(s => s._id === selectedSach.value);
  if (!sach) return [];
  return Array.from({ length: sach.SoChuong }, (_, i) => i + 1);
});

onMounted(() => {
  store.fetchBanDichs();
  store.fetchSachs();
});

function handleSachChange() {
  selectedChuong.value = '';
}

function handleFilter() {
  if (selectedBanDich.value && selectedSach.value && selectedChuong.value) {
    store.fetchCaus({
      banDichId: selectedBanDich.value,
      sachId: selectedSach.value,
      chuong: selectedChuong.value as number,
      page: 1
    });
  }
}

function handleSearch() {
  if (searchQuery.value) {
    store.searchCaus(searchQuery.value);
  }
}

function handlePageChange(page: number) {
  store.fetchCaus({
    banDichId: selectedBanDich.value,
    sachId: selectedSach.value,
    chuong: selectedChuong.value as number,
    page
  });
}
</script>

<style scoped>
.kinh-thanh { padding: 1rem; }
.filters { display: flex; gap: 1rem; margin-bottom: 1rem; flex-wrap: wrap; }
.filter-select { padding: 0.75rem; border: 1px solid #d1d5db; border-radius: 6px; min-width: 180px; }
.filter-select:disabled { background: #f3f4f6; cursor: not-allowed; }
.verses { display: flex; flex-direction: column; gap: 0.75rem; }
.verse { display: flex; gap: 0.75rem; padding: 0.5rem; border-radius: 4px; }
.verse:hover { background: #f9fafb; }
.verse-number { font-weight: bold; color: #3b82f6; min-width: 2rem; }
.verse-text { line-height: 1.6; }
.empty-state { text-align: center; padding: 3rem; color: #6b7280; }
</style>
