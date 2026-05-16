import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Product } from '../models/product';

export interface CartEntry {
  product: Product;
  quantity: number;
}

@Injectable({ providedIn: 'root' })
export class CartService {
  // BehaviorSubject keeps the latest cart state and pushes to all subscribers
  private cartSubject = new BehaviorSubject<CartEntry[]>([]);
  cart$ = this.cartSubject.asObservable();

  get items(): CartEntry[] {
    return this.cartSubject.value;
  }

  get totalItems(): number {
    return this.items.reduce((sum, e) => sum + e.quantity, 0);
  }

  get totalAmount(): number {
    return this.items.reduce((sum, e) => sum + e.product.price * e.quantity, 0);
  }

  addToCart(product: Product): void {
    const current = this.items;
    const existing = current.find(e => e.product.productId === product.productId);
    if (existing) {
      // already in cart — increase quantity
      existing.quantity++;
      this.cartSubject.next([...current]);
    } else {
      this.cartSubject.next([...current, { product, quantity: 1 }]);
    }
  }

  removeFromCart(productId: number): void {
    this.cartSubject.next(this.items.filter(e => e.product.productId !== productId));
  }

  updateQuantity(productId: number, quantity: number): void {
    if (quantity <= 0) { this.removeFromCart(productId); return; }
    const current = this.items.map(e =>
      e.product.productId === productId ? { ...e, quantity } : e
    );
    this.cartSubject.next(current);
  }

  clearCart(): void {
    this.cartSubject.next([]);
  }
}
