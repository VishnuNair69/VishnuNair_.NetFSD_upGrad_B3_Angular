"use strict";
// 1. Variable Declaration (Explicit Types)
const userName = "Geetha";
let age = 20;
const email = "geetha@example.com";
const isSubscribed = true;
// 2. Type Inference (No explicit types)
let city = "Hyderabad"; // inferred as string
let loginCount = 5; // inferred as number
// 3. let / const Usage
// age is declared with 'let' because it will be updated
age = age + 1; // Increment age
// 4. Template Literals
const userProfileMessage = `Hello ${userName}, you are ${age} years old and your email is ${email}.`;
// 5. Operators
// Check eligibility for premium plan
const isEligibleForPremium = age > 18 && isSubscribed;
// 6. Output using console.log()
console.log("User Name:", userName);
console.log("Age after increment:", age);
console.log("Email:", email);
console.log("Subscribed:", isSubscribed);
console.log("City (inferred):", city);
console.log("Login Count (inferred):", loginCount);
console.log("Profile Message:", userProfileMessage);
console.log("Eligible for Premium Plan:", isEligibleForPremium);
