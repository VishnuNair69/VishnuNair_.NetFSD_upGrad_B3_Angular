import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  name = '';
  email = '';
  password = '';
  role = 'Customer';
  error = '';
  loading = false;

  constructor(private http: HttpClient, private router: Router) {}

  register(): void {
    if (!this.name || !this.email || !this.password) { this.error = 'Please fill in all fields.'; return; }
    this.loading = true;
    this.error = '';
    const payload = { name: this.name, email: this.email, password: this.password, role: this.role };
    this.http.post<any>('http://localhost:8080/gateway/users/register', payload).subscribe({
      next: (user) => {
        localStorage.setItem('shopez_user', JSON.stringify(user));
        this.router.navigate(['/']);
      },
      error: (err) => { this.loading = false; this.error = err?.error?.message || 'Registration failed. Try again.'; }
    });
  }
}
