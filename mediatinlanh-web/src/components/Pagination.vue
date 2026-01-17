<template>
  <div class="pagination" v-if="totalPages > 1">
    <button 
      class="page-btn" 
      :disabled="currentPage <= 1"
      @click="$emit('change', currentPage - 1)"
    >
      ← Trước
    </button>
    
    <span class="page-info">
      Trang {{ currentPage }} / {{ totalPages }}
    </span>
    
    <button 
      class="page-btn" 
      :disabled="currentPage >= totalPages"
      @click="$emit('change', currentPage + 1)"
    >
      Sau →
    </button>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  currentPage: number;
  totalPages: number;
}>();

defineEmits<{
  change: [page: number];
}>();
</script>

<style scoped>
.pagination {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 1rem;
  margin-top: 1.5rem;
  padding: 1rem 0;
}

.page-btn {
  padding: 0.5rem 1rem;
  border: 1px solid #d1d5db;
  border-radius: 6px;
  background: white;
  cursor: pointer;
  transition: all 0.2s;
}

.page-btn:hover:not(:disabled) {
  background: #f3f4f6;
  border-color: #9ca3af;
}

.page-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.page-info {
  color: #6b7280;
}
</style>
