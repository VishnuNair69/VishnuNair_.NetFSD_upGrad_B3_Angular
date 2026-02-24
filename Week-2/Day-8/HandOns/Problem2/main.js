// main.js
import { calculateCartTotal, formatInvoiceLines } from "./cart.js";

// Store product objects in an array
const products = [
  { name: "Notebook", price: 5, quantity: 3 },
  { name: "Pen", price: 2, quantity: 5 },
  { name: "Bag", price: 25, quantity: 1 },
];

// Calculate total cart value
const total = calculateCartTotal(products);

// Format invoice using template literals + map()
const invoiceLines = formatInvoiceLines(products);

// Display formatted invoice (console output only)
const invoice = `
🧾 Invoice Summary
-------------------
${invoiceLines.join("\n")}
-------------------
Total Amount: $${total}
`;

console.log(invoice);