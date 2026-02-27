// Base class
class Payment {
  pay(amount) {
    console.log("Processing payment");
  }
}

// Subclasses
class CreditCardPayment extends Payment {
  pay(amount) {
    console.log(`Paid ₹${amount} using Credit Card`);
  }
}

class UPIPayment extends Payment {
  pay(amount) {
    console.log(`Paid ₹${amount} using UPI`);
  }
}

class CashPayment extends Payment {
  pay(amount) {
    console.log(`Paid ₹${amount} using Cash`);
  }
}

// Create objects
const credit = new CreditCardPayment();
const upi = new UPIPayment();
const cash = new CashPayment();

// Call pay() on each (polymorphism)
credit.pay(500);
upi.pay(500);
cash.pay(500);