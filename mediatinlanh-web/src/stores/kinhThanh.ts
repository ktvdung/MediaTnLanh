import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import apiService from '../services/api';
import type { KtCau, KtSach, KtBanDich, PaginatedResponse } from '../types';

export const useKinhThanhStore = defineStore('kinhThanh', () => {
  const caus = ref<KtCau[]>([]);
  const sachs = ref<KtSach[]>([]);
  const banDichs = ref<KtBanDich[]>([]);
  const loading = ref(false);
  const error = ref<string | null>(null);
  const total = ref(0);
  const page = ref(1);
  const limit = ref(50);

  const totalPages = computed(() => Math.ceil(total.value / limit.value));

  async function fetchBanDichs() {
    try {
      banDichs.value = await apiService.get<KtBanDich[]>('/kt-ban-dich');
    } catch (err: any) {
      console.error('Error fetching ban dich:', err);
    }
  }

  async function fetchSachs() {
    try {
      sachs.value = await apiService.get<KtSach[]>('/kt-sach');
    } catch (err: any) {
      console.error('Error fetching sach:', err);
    }
  }

  async function fetchCaus(params?: { banDichId?: string; sachId?: string; chuong?: number; page?: number; limit?: number }) {
    loading.value = true;
    error.value = null;
    try {
      const queryParams = new URLSearchParams();
      if (params?.banDichId) queryParams.append('banDichId', params.banDichId);
      if (params?.sachId) queryParams.append('sachId', params.sachId);
      if (params?.chuong) queryParams.append('chuong', params.chuong.toString());
      if (params?.page) queryParams.append('page', params.page.toString());
      if (params?.limit) queryParams.append('limit', params.limit.toString());

      const response = await apiService.get<PaginatedResponse<KtCau>>(`/kt-cau?${queryParams}`);
      caus.value = response.data;
      total.value = response.total;
      page.value = response.page;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tải dữ liệu';
    } finally {
      loading.value = false;
    }
  }

  async function searchCaus(keyword: string) {
    loading.value = true;
    error.value = null;
    try {
      const response = await apiService.get<PaginatedResponse<KtCau>>(`/kt-cau?search=${encodeURIComponent(keyword)}`);
      caus.value = response.data;
      total.value = response.total;
    } catch (err: any) {
      error.value = err.message || 'Lỗi khi tìm kiếm';
    } finally {
      loading.value = false;
    }
  }

  return {
    caus, sachs, banDichs, loading, error, total, page, limit, totalPages,
    fetchBanDichs, fetchSachs, fetchCaus, searchCaus
  };
});
