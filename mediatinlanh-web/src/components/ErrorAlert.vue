<template>
  <div v-if="message" class="error-alert" role="alert">
    <span class="error-icon">⚠️</span>
    <span class="error-message">{{ message }}</span>
    <button v-if="dismissible" class="dismiss-btn" @click="$emit('dismiss')" aria-label="Đóng">×</button>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  message: string | null;
  dismissible?: boolean;
}>();

defineEmits<{
  dismiss: [];
}>();
</script>

<style scoped>
.error-alert {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.75rem 1rem;
  background-color: #fee2e2;
  border: 1px solid #fecaca;
  border-radius: 6px;
  color: #dc2626;
  margin-bottom: 1rem;
}

.error-icon {
  flex-shrink: 0;
}

.error-message {
  flex: 1;
}

.dismiss-btn {
  background: none;
  border: none;
  font-size: 1.25rem;
  cursor: pointer;
  color: #dc2626;
  padding: 0 0.25rem;
}

.dismiss-btn:hover {
  color: #b91c1c;
}
</style>
