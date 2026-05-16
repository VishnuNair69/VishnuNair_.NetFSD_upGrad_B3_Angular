export interface CartItem {
  productId: number;
  quantity: number;
}

export interface CreateOrder {
  userId: number;
  cartItems: CartItem[];
}

export interface OrderItem {
  orderItemId: number;
  productId: number;
  productName: string;
  quantity: number;
  price: number;
}

export interface Order {
  orderId: number;
  userId: number;
  orderDate: string;
  totalAmount: number;
  items: OrderItem[];
}
