import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { User, LoginRequest, RegisterRequest } from '../models/user';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:8080/gateway/users';
  private currentUserSubject = new BehaviorSubject<User | null>(this.loadUser());
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {}

  private loadUser(): User | null {
    const stored = localStorage.getItem('shopez_user');
    return stored ? JSON.parse(stored) : null;
  }

  get currentUser(): User | null {
    return this.currentUserSubject.value;
  }

  get isLoggedIn(): boolean {
    return this.currentUserSubject.value !== null;
  }

  get isAdmin(): boolean {
    return this.currentUserSubject.value?.role === 'Admin';
  }

  // Login — calls backend GET /api/users/login or checks locally
  login(request: LoginRequest): Observable<User> {
  return this.http.post<User>(`${this.apiUrl}/login`, request).pipe(
    tap(user => {
      localStorage.setItem('shopez_user', JSON.stringify(user));
      this.currentUserSubject.next(user);
    })
  );
}

  // Register — POST to backend
  register(request: RegisterRequest): Observable<User> {
  return this.http.post<User>(`${this.apiUrl}/register`, request).pipe(
    tap(user => {
      localStorage.setItem('shopez_user', JSON.stringify(user));
      this.currentUserSubject.next(user);
    })
  );
}

  logout(): void {
    localStorage.removeItem('shopez_user');
    this.currentUserSubject.next(null);
  }
}
