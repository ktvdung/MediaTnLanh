<template>
  <div class="settings">
    <h1>Cài đặt</h1>
    
    <div class="settings-section">
      <h2>API Key</h2>
      <p class="description">Nhập API key để truy cập dữ liệu</p>
      
      <div class="form-group">
        <input
          type="text"
          v-model="apiKey"
          placeholder="Nhập API key..."
          class="input"
        />
        <button @click="saveApiKey" class="btn btn-primary">Lưu</button>
        <button @click="clearApiKey" class="btn btn-secondary">Xóa</button>
      </div>
      
      <p v-if="message" :class="['message', messageType]">{{ message }}</p>
    </div>

    <div class="settings-section">
      <h2>Trạng thái kết nối</h2>
      <div class="status-item">
        <span>API Server:</span>
        <span :class="['status-badge', apiStatus]">{{ apiStatusText }}</span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import apiService from '../services/api';

const apiKey = ref('');
const message = ref('');
const messageType = ref<'success' | 'error'>('success');
const apiStatus = ref<'checking' | 'connected' | 'disconnected'>('checking');
const apiStatusText = ref('Đang kiểm tra...');

onMounted(() => {
  apiKey.value = apiService.getApiKey() || '';
  checkApiStatus();
});

async function checkApiStatus() {
  try {
    await apiService.get('/health');
    apiStatus.value = 'connected';
    apiStatusText.value = 'Đã kết nối';
  } catch {
    apiStatus.value = 'disconnected';
    apiStatusText.value = 'Không kết nối được';
  }
}

function saveApiKey() {
  if (apiKey.value.trim()) {
    apiService.setApiKey(apiKey.value.trim());
    message.value = 'Đã lưu API key';
    messageType.value = 'success';
  } else {
    message.value = 'Vui lòng nhập API key';
    messageType.value = 'error';
  }
  setTimeout(() => message.value = '', 3000);
}

function clearApiKey() {
  apiService.clearApiKey();
  apiKey.value = '';
  message.value = 'Đã xóa API key';
  messageType.value = 'success';
  setTimeout(() => message.value = '', 3000);
}
</script>

<style scoped>
.settings { padding: 1rem; max-width: 600px; }
.settings-section { background: white; padding: 1.5rem; border: 1px solid #e5e7eb; border-radius: 12px; margin-bottom: 1.5rem; }
.settings-section h2 { margin: 0 0 0.5rem; font-size: 1.25rem; }
.description { color: #6b7280; margin-bottom: 1rem; }
.form-group { display: flex; gap: 0.5rem; flex-wrap: wrap; }
.input { flex: 1; min-width: 200px; padding: 0.75rem; border: 1px solid #d1d5db; border-radius: 6px; }
.btn { padding: 0.75rem 1.5rem; border: none; border-radius: 6px; cursor: pointer; font-size: 1rem; }
.btn-primary { background: #3b82f6; color: white; }
.btn-primary:hover { background: #2563eb; }
.btn-secondary { background: #e5e7eb; color: #374151; }
.btn-secondary:hover { background: #d1d5db; }
.message { margin-top: 1rem; padding: 0.75rem; border-radius: 6px; }
.message.success { background: #dcfce7; color: #166534; }
.message.error { background: #fee2e2; color: #dc2626; }
.status-item { display: flex; justify-content: space-between; align-items: center; padding: 0.75rem 0; }
.status-badge { padding: 0.25rem 0.75rem; border-radius: 9999px; font-size: 0.875rem; }
.status-badge.checking { background: #fef3c7; color: #92400e; }
.status-badge.connected { background: #dcfce7; color: #166534; }
.status-badge.disconnected { background: #fee2e2; color: #dc2626; }
</style>
