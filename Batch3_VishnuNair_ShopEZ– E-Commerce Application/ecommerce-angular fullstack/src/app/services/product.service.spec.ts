import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';

import { ProductService } from './product.service';
import { Product } from '../models/product';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create service', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch products', () => {
    const mockProducts: Product[] = [
      {
        productId: 1,
        name: 'Laptop',
        description: 'Gaming Laptop',
        price: 50000,
        imageUrl: 'test.jpg',
        stock: 10
      }
    ];

    service.getAll().subscribe((products: Product[]) => {
      expect(products.length).toBe(1);
      expect(products[0].name).toBe('Laptop');
    });

    const req = httpMock.expectOne(
      'http://localhost:8080/gateway/products'
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockProducts);
  });
});