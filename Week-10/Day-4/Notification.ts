export {};

function getWelcomeMessage(name: string): string {
    return `Welcome ${name}!`;
}

function getUserInfo(name: string, age?: number): string {
    if (age !== undefined) {
        return `User ${name} is ${age} years old.`;
    }
    return `User ${name} has not provided age.`;
}


function getSubscriptionStatus(name: string, isSubscribed: boolean = false): string {
    if (isSubscribed) {
        return `${name} is subscribed to premium services.`;
    }
    return `${name} is not subscribed.`;
}

const isEligibleForPremium = (age: number): boolean => {
    return age > 18;
};


const getAccountStatus = (name: string): string => {
    return `Account for ${name} is active.`;
};

const notificationService = {
    appName: "MyApp",

    sendNotification: (message: string): string => {
        // Arrow function uses lexical this (from outer scope)
        return `[${notificationService.appName}] ${message}`;
    }
};

console.log(getWelcomeMessage("Geetha"));

console.log(getUserInfo("Geetha", 21));
console.log(getUserInfo("Geetha"));

console.log(getSubscriptionStatus("Geetha", true));
console.log(getSubscriptionStatus("Geetha"));

console.log("Eligible for Premium:", isEligibleForPremium(21));

console.log(getAccountStatus("Geetha"));

console.log(notificationService.sendNotification("You have a new message!"));