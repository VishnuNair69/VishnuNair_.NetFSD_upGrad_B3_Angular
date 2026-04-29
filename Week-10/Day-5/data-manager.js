"use strict";
// 1. Generic Function
function getFirstElement(items) {
    return items[0];
}
// 3. Generic Class
class DataManager {
    items = [];
    add(item) {
        this.items.push(item);
    }
    getAll() {
        return this.items;
    }
}
// 5. Use Case Implementation
// User Data Manager
const userManager = new DataManager();
userManager.add({ id: 1, name: "Geetha" });
userManager.add({ id: 2, name: "Rahul" });
// Product Data Manager
const productManager = new DataManager();
productManager.add({ id: 101, title: "Laptop" });
productManager.add({ id: 102, title: "Mobile" });
// Testing
console.log("Users:", userManager.getAll());
console.log("Products:", productManager.getAll());
// Using Generic Function
console.log("First User:", getFirstElement(userManager.getAll()));
console.log("First Product:", getFirstElement(productManager.getAll()));
