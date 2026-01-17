<template>
  <div class="search-box">
    <input
      type="text"
      :value="modelValue"
      :placeholder="placeholder"
      @input="$emit('update:modelValue', ($event.target as HTMLInputElement).value)"
      @keyup.enter="$emit('search')"
      class="search-input"
    />
    <button class="search-btn" @click="$emit('search')">
      🔍 Tìm kiếm
    </button>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  modelValue: string;
  placeholder?: string;
}>();

defineEmits<{
  'update:modelValue': [value: string];
  search: [];
}>();
</script>

<style scoped>
.search-box {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.search-input {
  flex: 1;
  padding: 0.75rem 1rem;
  border: 1px solid #d1d5db;
  border-radius: 6px;
  font-size: 1rem;
}

.search-input:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 3px rgba(59, 130, 246, 0.1);
}

.search-btn {
  padding: 0.75rem 1.5rem;
  background: #3b82f6;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  transition: background 0.2s;
}

.search-btn:hover {
  background: #2563eb;
}
</style>
