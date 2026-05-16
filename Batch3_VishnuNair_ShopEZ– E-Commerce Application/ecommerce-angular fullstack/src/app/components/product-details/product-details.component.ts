import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: Product | null = null;
  loading = true;
  error = '';
  successMsg = '';
  quantity = 1;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    public cartService: CartService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getById(id).subscribe({
      next: (p) => { this.product = p; this.loading = false; },
      error: () => { this.error = 'Product not found.'; this.loading = false; }
    });
  }

  addToCart(): void {
    if (!this.authService.isLoggedIn) {
      this.error = 'Please login to add items to cart.';
      return;
    }
    if (!this.product) return;
    for (let i = 0; i < this.quantity; i++) {
      this.cartService.addToCart(this.product);
    }
    this.successMsg = `Added ${this.quantity} × "${this.product.name}" to cart!`;
    setTimeout(() => this.successMsg = '', 3000);
  }

  getImageUrl(url: string): string {
    if (!url || url.startsWith('/images/')) return 'https://placehold.co/600x400/FAF7F2/C8A96E?text=Product';
    return url;
  }

  increment(): void { if (this.product && this.quantity < this.product.stock) this.quantity++; }
  decrement(): void { if (this.quantity > 1) this.quantity--; }
}
