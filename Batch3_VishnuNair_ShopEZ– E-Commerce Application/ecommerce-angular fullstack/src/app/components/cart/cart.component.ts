import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService, CartEntry } from '../../services/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems: CartEntry[] = [];

  constructor(public cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.cart$.subscribe(items => {
      this.cartItems = items;
    });
  }

  remove(productId: number): void {
    this.cartService.removeFromCart(productId);
  }

  updateQty(productId: number, qty: number): void {
    this.cartService.updateQuantity(productId, qty);
  }

  clearCart(): void {
    this.cartService.clearCart();
  }

  getImageUrl(url: string): string {
    if (!url || url.startsWith('/images/')) return 'https://placehold.co/80x80/FAF7F2/C8A96E?text=Item';
    return url;
  }
}
