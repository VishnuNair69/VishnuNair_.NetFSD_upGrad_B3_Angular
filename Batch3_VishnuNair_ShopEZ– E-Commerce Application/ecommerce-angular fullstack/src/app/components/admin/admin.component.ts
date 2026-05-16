import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { OrderService } from '../../services/order.service';
import { Product } from '../../models/product';
import { Order } from '../../models/order';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  activeTab: 'products' | 'orders' = 'products';
  products: Product[] = [];
  orders: Order[] = [];
  loading = false;
  successMsg = '';
  error = '';
  showForm = false;
  editingProduct: Product | null = null;
  productForm: FormGroup;

  constructor(
    private productService: ProductService,
    private orderService: OrderService,
    private fb: FormBuilder
  ) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(1)]],
      imageUrl: [''],
      stock: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void { this.loadProducts(); }

  loadProducts(): void {
    this.loading = true;
    this.productService.getAll().subscribe({
      next: (p) => { this.products = p; this.loading = false; },
      error: () => { this.error = 'Failed to load products.'; this.loading = false; }
    });
  }

  loadOrders(): void {
    this.loading = true;
    this.orderService.getAll().subscribe({
      next: (o) => { this.orders = o; this.loading = false; },
      error: () => { this.error = 'Failed to load orders.'; this.loading = false; }
    });
  }

  switchTab(tab: 'products' | 'orders'): void {
    this.activeTab = tab;
    this.error = '';
    this.successMsg = '';
    if (tab === 'orders') this.loadOrders();
    else this.loadProducts();
  }

  openAddForm(): void {
    this.editingProduct = null;
    this.productForm.reset({ price: 0, stock: 0, imageUrl: '' });
    this.showForm = true;
  }

  openEditForm(product: Product): void {
    this.editingProduct = product;
    this.productForm.patchValue(product);
    this.showForm = true;
  }

  closeForm(): void { this.showForm = false; this.editingProduct = null; }

  submitProduct(): void {
    if (this.productForm.invalid) return;
    const data = this.productForm.value;
    if (this.editingProduct) {
      // Update
      this.productService.update(this.editingProduct.productId, data).subscribe({
        next: () => { this.successMsg = 'Product updated!'; this.closeForm(); this.loadProducts(); setTimeout(() => this.successMsg = '', 3000); },
        error: () => { this.error = 'Failed to update product.'; }
      });
    } else {
      // Create
      this.productService.create(data).subscribe({
        next: () => { this.successMsg = 'Product added!'; this.closeForm(); this.loadProducts(); setTimeout(() => this.successMsg = '', 3000); },
        error: () => { this.error = 'Failed to add product.'; }
      });
    }
  }

  deleteProduct(product: Product): void {
    if (!confirm(`Delete "${product.name}"?`)) return;
    this.productService.delete(product.productId).subscribe({
      next: () => { this.successMsg = 'Product deleted!'; this.loadProducts(); setTimeout(() => this.successMsg = '', 3000); },
      error: () => { this.error = 'Failed to delete. Product may be used in existing orders.'; }
    });
  }
}
