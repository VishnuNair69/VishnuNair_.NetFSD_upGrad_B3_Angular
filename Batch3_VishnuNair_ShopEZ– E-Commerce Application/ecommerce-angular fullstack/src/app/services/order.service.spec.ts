import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';

import { OrderService } from './order.service';

describe('OrderService', () => {
  let service: OrderService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    service = TestBed.inject(OrderService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create service', () => {
    expect(service).toBeTruthy();
  });

  it('should create an order', () => {
    const createOrderRequest = {
      userId: 1,
      cartItems: [
        {
          productId: 1,
          quantity: 2
        }
      ]
    };

    const mockResponse = {
      orderId: 1,
      userId: 1,
      totalAmount: 2500,
      orderDate: new Date().toISOString()
    };

    service.createOrder(createOrderRequest).subscribe((response: any) => {
      expect(response.orderId).toBe(1);
    });

    const req = httpMock.expectOne(
      'http://localhost:8080/gateway/orders'
    );

    expect(req.request.method).toBe('POST');

    req.flush(mockResponse);
  });

  it('should fetch all orders', () => {
    const mockOrders = [
      {
        orderId: 1,
        userId: 1,
        totalAmount: 2500,
        orderDate: new Date().toISOString()
      }
    ];

    service.getAll().subscribe((orders: any[]) => {
      expect(orders.length).toBe(1);
      expect(orders[0].orderId).toBe(1);
    });

    const req = httpMock.expectOne(
      'http://localhost:8080/gateway/orders'
    );

    expect(req.request.method).toBe('GET');

    req.flush(mockOrders);
  });
});