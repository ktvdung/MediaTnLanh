<template>
  <div class="thanh-ca">
    <h1>Thánh Ca</h1>
    
    <ErrorAlert :message="store.error" dismissible @dismiss="store.error = null" />
    
    <div class="filters">
      <SearchBox v-model="searchQuery" placeholder="Tìm kiếm thánh ca..." @search="handleSearch" />
      <select v-model="selectedTheLoai" @change="handleFilter" class="filter-select">
        <option value="">Tất cả thể loại</option>
        <option v-for="tl in store.theLoais" :key="tl._id" :value="tl._id">{{ tl.TenTheLoai }}</option>
      </select>
    </div>

    <LoadingSpinner v-if="store.loading" message="Đang tải..." />
    
    <div v-else-if="store.items.length === 0" class="empty-state">
      <p>Không có dữ liệu</p>
    </div>
    
    <div v-else class="list">
      <div v-for="item in store.items" :key="item._id" class="list-item" @click="showDetail(item)">
        <div class="item-header">
          <span class="item-code">{{ item.MaTc }}</span>
          <span class="item-title">{{ item.TenTc }}</span>
        </div>
        <div class="item-meta">
          <span v-if="item.TacGia">Tác giả: {{ item.TacGia }}</span>
        </div>
      </div>
    </div>

    <Pagination :currentPage="store.page" :totalPages="store.totalPages" @change="handlePageChange" />

    <!-- Detail Modal -->
    <div v-if="selectedItem" class="modal-overlay" @click.self="selectedItem = null">
      <div class="modal">
        <div class="modal-header">
          <h2>{{ selectedItem.MaTc }} - {{ selectedItem.TenTc }}</h2>
          <button class="close-btn" @click="selectedItem = null">×</button>
        </div>
        <div class="modal-body">
          <p v-if="selectedItem.TacGia"><strong>Tác giả:</strong> {{ selectedItem.TacGia }}</p>
          <div v-if="selectedItem.NoiDung" class="content-box">
            <pre>{{ selectedItem.NoiDung }}</pre>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useThanhCaStore } from '../stores/thanhCa';
import type { TcThanhCa } from '../types';
import ErrorAlert from '../components/ErrorAlert.vue';
import LoadingSpinner from '../components/LoadingSpinner.vue';
import SearchBox from '../components/SearchBox.vue';
import Pagination from '../components/Pagination.vue';

const store = useThanhCaStore();
const searchQuery = ref('');
const selectedTheLoai = ref('');
const selectedItem = ref<TcThanhCa | null>(null);

onMounted(() => {
  store.fetchItems();
  store.fetchTheLoais();
});

function handleSearch() {
  store.fetchItems({ search: searchQuery.value, theLoaiId: selectedTheLoai.value, page: 1 });
}

function handleFilter() {
  store.fetchItems({ search: searchQuery.value, theLoaiId: selectedTheLoai.value, page: 1 });
}

function handlePageChange(page: number) {
  store.fetchItems({ search: searchQuery.value, theLoaiId: selectedTheLoai.value, page });
}

function showDetail(item: TcThanhCa) {
  selectedItem.value = item;
}
</script>

<style scoped>
.thanh-ca { padding: 1rem; }
.filters { display: flex; gap: 1rem; margin-bottom: 1rem; flex-wrap: wrap; }
.filter-select { padding: 0.75rem; border: 1px solid #d1d5db; border-radius: 6px; min-width: 200px; }
.list { display: flex; flex-direction: column; gap: 0.5rem; }
.list-item { padding: 1rem; border: 1px solid #e5e7eb; border-radius: 8px; cursor: pointer; transition: all 0.2s; }
.list-item:hover { border-color: #3b82f6; box-shadow: 0 2px 8px rgba(0,0,0,0.1); }
.item-header { display: flex; gap: 1rem; align-items: baseline; }
.item-code { font-weight: bold; color: #3b82f6; }
.item-title { font-size: 1.1rem; }
.item-meta { font-size: 0.875rem; color: #6b7280; margin-top: 0.25rem; }
.empty-state { text-align: center; padding: 3rem; color: #6b7280; }
.modal-overlay { position: fixed; top: 0; left: 0; right: 0; bottom: 0; background: rgba(0,0,0,0.5); display: flex; align-items: center; justify-content: center; z-index: 1000; }
.modal { background: white; border-radius: 12px; max-width: 600px; width: 90%; max-height: 80vh; overflow: auto; }
.modal-header { display: flex; justify-content: space-between; align-items: center; padding: 1rem 1.5rem; border-bottom: 1px solid #e5e7eb; }
.modal-header h2 { margin: 0; font-size: 1.25rem; }
.close-btn { background: none; border: none; font-size: 1.5rem; cursor: pointer; color: #6b7280; }
.modal-body { padding: 1.5rem; }
.content-box { background: #f9fafb; padding: 1rem; border-radius: 6px; margin-top: 1rem; }
.content-box pre { white-space: pre-wrap; margin: 0; font-family: inherit; }
</style>
