import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import apiService from '../services/api';
import type { DktCauHoi, DktChuDe, DktLoai, PaginatedResponse } from '../types';

export const useQuizStore = defineStore('quiz', () => {
  const cauHois = ref<DktCauHoi[]>([]);
  const chuDes = ref<DktChuDe[]>([]);
  const loais = ref<DktLoai[]>([]);
  const currentItem = ref<DktCauHoi | null>(null);
  const loading = ref(false);
  const error = ref<string | null>(null);
  const total = ref(0);
  const page = ref(1);
  const limit = ref(20);

  const totalPages = computed(() => Math.ceil(total.value / limit.value));

  async function fetchLoais() {
    try {
      loais.value = await apiService.get<DktLoai[]>('/dkt-loai');
    } catch (err: any) {
      console.error('Error fetching loai:', err);
    }
  }

  async function fetchChuDes(loaiId?: string) {
    try {
      const url = loaiId ? `/dkt-chu-de?loaiId=${loaiId}` : '/dkt-chu-de';
      chuDes.value = await apiService.get<DktChuDe[]>(url);
    } catch (err: any) {
      console.error('Error fetching chu de:', err);
    }
  }

  async function fetchCauHois(params?: { chuDeId?: string; page?: number; limit?: number }) {
    loading.value = true;
    error.value = null;
    try {
      const queryParams = new URLSearchParams();
      if (params?.chuDeId) queryParams.append('chuDeId', params.chuDeId);
      if (params?.page) queryParams.append('page', params.page.toString());
      if (params?.limit) queryParams.append('limit', params.limit.toString());

      const response = await apiService.get<PaginatedResponse<DktCauHoi>>(`/dkt-cau-hoi?${queryParams}`);
      cauHois.value = response.data;
      total.value = response.total;
      page.value = response.page;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tải dữ liệu';
    } finally {
      loading.value = false;
    }
  }

  async function fetchById(id: string) {
    loading.value = true;
    error.value = null;
    try {
      currentItem.value = await apiService.get<DktCauHoi>(`/dkt-cau-hoi/${id}`);
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tải dữ liệu';
    } finally {
      loading.value = false;
    }
  }

  async function create(data: Partial<DktCauHoi>) {
    loading.value = true;
    error.value = null;
    try {
      const created = await apiService.post<DktCauHoi>('/dkt-cau-hoi', data);
      cauHois.value.unshift(created);
      return created;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tạo mới';
      throw err;
    } finally {
      loading.value = false;
    }
  }

  async function update(id: string, data: Partial<DktCauHoi>) {
    loading.value = true;
    error.value = null;
    try {
      const updated = await apiService.put<DktCauHoi>(`/dkt-cau-hoi/${id}`, data);
      const index = cauHois.value.findIndex(item => item._id === id);
      if (index !== -1) cauHois.value[index] = updated;
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
      await apiService.delete(`/dkt-cau-hoi/${id}`);
      cauHois.value = cauHois.value.filter(item => item._id !== id);
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi xóa';
      throw err;
    } finally {
      loading.value = false;
    }
  }

  return {
    cauHois, chuDes, loais, currentItem, loading, error, total, page, limit, totalPages,
    fetchLoais, fetchChuDes, fetchCauHois, fetchById, create, update, remove
  };
});
