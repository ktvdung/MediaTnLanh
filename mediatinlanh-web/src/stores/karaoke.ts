import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import apiService from '../services/api';
import type { KaBaiHat, KaTheLoai, PaginatedResponse } from '../types';

export const useKaraokeStore = defineStore('karaoke', () => {
  const items = ref<KaBaiHat[]>([]);
  const theLoais = ref<KaTheLoai[]>([]);
  const currentItem = ref<KaBaiHat | null>(null);
  const loading = ref(false);
  const error = ref<string | null>(null);
  const total = ref(0);
  const page = ref(1);
  const limit = ref(20);

  const totalPages = computed(() => Math.ceil(total.value / limit.value));

  async function fetchItems(params?: { page?: number; limit?: number; search?: string; theLoaiId?: string }) {
    loading.value = true;
    error.value = null;
    try {
      const queryParams = new URLSearchParams();
      if (params?.page) queryParams.append('page', params.page.toString());
      if (params?.limit) queryParams.append('limit', params.limit.toString());
      if (params?.search) queryParams.append('search', params.search);
      if (params?.theLoaiId) queryParams.append('theLoaiId', params.theLoaiId);

      const response = await apiService.get<PaginatedResponse<KaBaiHat>>(`/ka-bai-hat?${queryParams}`);
      items.value = response.data;
      total.value = response.total;
      page.value = response.page;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tải dữ liệu';
    } finally {
      loading.value = false;
    }
  }

  async function fetchTheLoais() {
    try {
      theLoais.value = await apiService.get<KaTheLoai[]>('/ka-the-loai');
    } catch (err: any) {
      console.error('Error fetching the loai:', err);
    }
  }

  async function fetchById(id: string) {
    loading.value = true;
    error.value = null;
    try {
      currentItem.value = await apiService.get<KaBaiHat>(`/ka-bai-hat/${id}`);
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tải dữ liệu';
    } finally {
      loading.value = false;
    }
  }

  async function create(data: Partial<KaBaiHat>) {
    loading.value = true;
    error.value = null;
    try {
      const created = await apiService.post<KaBaiHat>('/ka-bai-hat', data);
      items.value.unshift(created);
      return created;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tạo mới';
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function update(id: string, data: Partial<KaBaiHat>) {
    loading.value = true;
    error.value = null;
    try {
      const updated = await apiService.put<KaBaiHat>(`/ka-bai-hat/${id}`, data);
      const index = items.value.findIndex(item => item._id === id);
      if (index !== -1) items.value[index] = updated;
      return updated;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi cập nhật';
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function remove(id: string) {
    loading.value = true;
    error.value = null;
    try {
      await apiService.delete(`/ka-bai-hat/${id}`);
      items.value = items.value.filter(item => item._id !== id);
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi xóa';
      throw err;
    } finally {
      loading.value = false;
    }
  }

  return {
    items, theLoais, currentItem, loading, error, total, page, limit, totalPages,
    fetchItems, fetchTheLoais, fetchById, create, update, remove
  };
});
