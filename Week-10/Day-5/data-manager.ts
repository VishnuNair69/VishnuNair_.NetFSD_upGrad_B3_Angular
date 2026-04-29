// 1. Generic Function
function getFirstElement<T>(items: T[]): T {
  return items[0];
}

// 2. Generic Interface
interface Repository<T> {
  add(item: T): void;
  getAll(): T[];
}

// 3. Generic Class
class DataManager<T> implements Repository<T> {
  private items: T[] = [];

  add(item: T): void {
    this.items.push(item);
  }

  getAll(): T[] {
    return this.items;
  }
}

// 4. Models
interface User {
  id: number;
  name: string;
}

interface Product {
  id: number;
  title: string;
}


// User Data Manager
const userManager = new DataManager<User>();

userManager.add({ id: 1, name: "Geetha" });
userManager.add({ id: 2, name: "Chandhu" });

// Product Data Manager
const productManager = new DataManager<Product>();

productManager.add({ id: 101, title: "Laptop" });
productManager.add({ id: 102, title: "Mobile" });

// Testing

console.log("Users:", userManager.getAll());
console.log("Products:", productManager.getAll());

// Using Generic Function
console.log("First User:", getFirstElement(userManager.getAll()));
console.log("First Product:", getFirstElement(productManager.getAll()));