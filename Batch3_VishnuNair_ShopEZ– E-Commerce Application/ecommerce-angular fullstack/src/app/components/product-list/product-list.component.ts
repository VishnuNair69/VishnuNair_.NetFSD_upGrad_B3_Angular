import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  searchTerm = '';
  loading = true;
  error = '';
  successMsg = '';

  constructor(
    private productService: ProductService,
    public cartService: CartService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: (products) => {
        this.products = products;
        this.filteredProducts = products;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load products. Make sure the backend is running.';
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    const term = this.searchTerm.toLowerCase();
    this.filteredProducts = this.products.filter(p =>
      p.name.toLowerCase().includes(term) ||
      p.description.toLowerCase().includes(term)
    );
  }

  addToCart(product: Product): void {
    if (!this.authService.isLoggedIn) {
      this.error = 'Please login to add items to cart.';
      setTimeout(() => this.error = '', 3000);
      return;
    }
    this.cartService.addToCart(product);
    this.successMsg = `"${product.name}" added to cart!`;
    setTimeout(() => this.successMsg = '', 2500);
  }

  getImageUrl(url: string): string {
    // Fallback image if no real image
    if (!url || url.startsWith('/images/')) {
      return 'https://placehold.co/400x300/FAF7F2/C8A96E?text=Product';
    }
    return url;
  }
}
