// Base class
class Vehicle {
  constructor(brand, speed) {
    this.brand = brand;
    this.speed = speed;
  }

  displayInfo() {
    console.log(`Brand: ${this.brand}, Speed: ${this.speed} km/h`);
  }
}

// Derived class
class Car extends Vehicle {
  constructor(brand, speed, fuelType) {
    super(brand, speed);
    this.fuelType = fuelType;
  }

  showCarDetails() {
    console.log(`Fuel Type: ${this.fuelType}`);
  }
}

// Create one Car object
const myCar = new Car("Toyota", 120, "Petrol");

// Call parent method
myCar.displayInfo();

// Call child method
myCar.showCarDetails();