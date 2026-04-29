// 1. Function with Required Parameter
function getWelcomeMessage(name) {
    return `Welcome ${name}!`;
}
// 2. Optional Parameter
function getUserInfo(name, age) {
    if (age !== undefined) {
        return `User ${name} is ${age} years old.`;
    }
    return `User ${name} has not provided age.`;
}
// 3. Default Parameter
function getSubscriptionStatus(name, isSubscribed = false) {
    if (isSubscribed) {
        return `${name} is subscribed to premium services.`;
    }
    return `${name} is not subscribed.`;
}
// 4. Function with Boolean Return Type (Arrow Function)
const isEligibleForPremium = (age) => {
    return age > 18;
};
// 5. Arrow Function Example
const getAccountStatus = (name) => {
    return `Account for ${name} is active.`;
};
// 6. Lexical 'this' using Arrow Function
const notificationService = {
    appName: "MyApp",
    sendNotification: (message) => {
        // Arrow function uses lexical this (from outer scope)
        return `[${notificationService.appName}] ${message}`;
    }
};
// 7. Execution (Calling all functions)
console.log(getWelcomeMessage("Geetha"));
console.log(getUserInfo("Geetha", 21));
console.log(getUserInfo("Geetha"));
console.log(getSubscriptionStatus("Geetha", true));
console.log(getSubscriptionStatus("Geetha"));
console.log("Eligible for Premium:", isEligibleForPremium(21));
console.log(getAccountStatus("Geetha"));
console.log(notificationService.sendNotification("You have a new message!"));
export {};
