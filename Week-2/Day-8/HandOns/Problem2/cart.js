// cart.js

// Arrow function to calculate total cart value
export const calculateCartTotal = (products) => {
  return products.reduce((total, item) => {
    return total + item.price * item.quantity;
  }, 0);
};

// Optional: helper to format invoice lines using map()
export const formatInvoiceLines = (products) => {
  return products.map(
    (p, i) => `${i + 1}. ${p.name} - $${p.price} x ${p.quantity} = $${p.price * p.quantity}`
  );
};