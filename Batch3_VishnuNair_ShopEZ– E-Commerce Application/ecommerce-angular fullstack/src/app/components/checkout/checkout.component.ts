import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { OrderService } from '../../services/order.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {
  cartItems$ = this.cartService.cart$;
  loading = false;
  success = false;
  error = '';
  placedOrderId: number | null = null;

  constructor(
    public cartService: CartService,
    private orderService: OrderService,
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (!this.authService.isLoggedIn) { this.router.navigate(['/login']); }
  }

  placeOrder(): void {
    if (this.cartService.items.length === 0) return;
    this.loading = true;
    this.error = '';
    const userId = this.authService.currentUser!.userId;
    const orderPayload = {
      userId,
      cartItems: this.cartService.items.map(e => ({ productId: e.product.productId, quantity: e.quantity }))
    };
    this.orderService.createOrder(orderPayload).subscribe({
      next: (order) => { this.loading = false; this.success = true; this.placedOrderId = order.orderId; this.cartService.clearCart(); },
      error: (err) => { this.loading = false; this.error = err?.error?.message || 'Failed to place order. Please try again.'; }
    });
  }

  getImageUrl(url: string): string {
    if (!url || url.startsWith('/images/')) return 'https://placehold.co/60x60/FAF7F2/C8A96E?text=Item';
    return url;
  }
}
