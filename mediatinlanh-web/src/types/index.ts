// Common types for MediaTinLanh

export interface PaginatedResponse<T> {
  data: T[];
  total: number;
  page: number;
  limit: number;
  totalPages: number;
}

export interface ApiError {
  message: string;
  status?: number;
}

// Thánh Ca types
export interface TcThanhCa {
  _id: string;
  MaTc: string;
  TenTc: string;
  TacGia?: string;
  TheLoaiId?: string;
  NgonNguId?: string;
  NoiDung?: string;
  TrangThai?: number;
  createdAt?: string;
  updatedAt?: string;
}

export interface TcTheLoai {
  _id: string;
  TenTheLoai: string;
  MoTa?: string;
}

export interface TcLoi {
  _id: string;
  ThanhCaId: string;
  NgonNguId: string;
  NoiDung: string;
}

// Kinh Thánh types
export interface KtBanDich {
  _id: string;
  TenBanDich: string;
  MaBanDich: string;
  NgonNguId?: string;
}

export interface KtSach {
  _id: string;
  TenSach: string;
  MaSach: string;
  ThuTu: number;
  SoChuong: number;
}

export interface KtCau {
  _id: string;
  BanDichId: string;
  SachId: string;
  Chuong: number;
  Cau: number;
  NoiDung: string;
}

// Karaoke types
export interface KaTheLoai {
  _id: string;
  TenTheLoai: string;
  MoTa?: string;
}

export interface KaBaiHat {
  _id: string;
  TenBaiHat: string;
  TheLoaiId?: string;
  TacGia?: string;
  LinkVideo?: string;
  TrangThai?: number;
}

// Quiz types
export interface DktLoai {
  _id: string;
  TenLoai: string;
  MoTa?: string;
}

export interface DktChuDe {
  _id: string;
  TenChuDe: string;
  LoaiId?: string;
  MoTa?: string;
}

export interface DktCauHoi {
  _id: string;
  ChuDeId: string;
  NoiDung: string;
  DapAnA: string;
  DapAnB: string;
  DapAnC: string;
  DapAnD: string;
  DapAnDung: string;
  GiaiThich?: string;
}

// NgonNgu type
export interface NgonNgu {
  _id: string;
  TenNgonNgu: string;
  MaNgonNgu: string;
}
