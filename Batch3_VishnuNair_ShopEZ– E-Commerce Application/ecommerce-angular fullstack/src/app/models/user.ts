export interface User {
  userId: number;
  name: string;
  email: string;
  role: string; // 'Admin' | 'Customer'
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  password: string;
  role: string;
}
