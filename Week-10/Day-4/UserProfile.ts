const userName: string = "Geetha";
let age: number = 20;
const email: string = "geetha@example.com";
const isSubscribed: boolean = true;

let city = "Hyderabad";      
let loginCount = 5;         

age = age + 1; 

const userProfileMessage: string = `Hello ${userName}, you are ${age} years old and your email is ${email}.`;
const isEligibleForPremium: boolean = age > 18 && isSubscribed;

console.log("User Name:", userName);
console.log("Age after increment:", age);
console.log("Email:", email);
console.log("Subscribed:", isSubscribed);
console.log("City (inferred):", city);
console.log("Login Count (inferred):", loginCount);
console.log("Profile Message:", userProfileMessage);
console.log("Eligible for Premium Plan:", isEligibleForPremium);