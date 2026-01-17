import axios from 'axios';
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:3000/api';

class ApiService {
  private client: AxiosInstance;
  private apiKey: string | null = null;

  constructor() {
    this.client = axios.create({
      baseURL: API_BASE_URL,
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // Request interceptor to add API key
    this.client.interceptors.request.use((config) => {
      if (this.apiKey) {
        config.headers['X-API-KEY'] = this.apiKey;
      }
      return config;
    });

    // Response interceptor for error handling
    this.client.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response?.status === 401) {
          console.error('Unauthorized: Invalid or missing API key');
        }
        return Promise.reject(error);
      }
    );

    // Load API key from localStorage
    const savedApiKey = localStorage.getItem('apiKey');
    if (savedApiKey) {
      this.apiKey = savedApiKey;
    }
  }

  setApiKey(key: string): void {
    this.apiKey = key;
    localStorage.setItem('apiKey', key);
  }

  clearApiKey(): void {
    this.apiKey = null;
    localStorage.removeItem('apiKey');
  }

  getApiKey(): string | null {
    return this.apiKey;
  }

  async get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const response: AxiosResponse<T> = await this.client.get(url, config);
    return response.data;
  }

  async post<T>(url: string, data?: unknown, config?: AxiosRequestConfig): Promise<T> {
    const response: AxiosResponse<T> = await this.client.post(url, data, config);
    return response.data;
  }

  async put<T>(url: string, data?: unknown, config?: AxiosRequestConfig): Promise<T> {
    const response: AxiosResponse<T> = await this.client.put(url, data, config);
    return response.data;
  }

  async delete<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const response: AxiosResponse<T> = await this.client.delete(url, config);
    return response.data;
  }
}

export const apiService = new ApiService();
export default apiService;
