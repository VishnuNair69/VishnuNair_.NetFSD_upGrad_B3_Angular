// 1. Base Class: Employee
class Employee {
    id;
    name;
    salary;
    // Constructor
    constructor(id, name, salary) {
        this.id = id;
        this.name = name;
        this.salary = salary;
    }
    // 2. Getter
    getSalary() {
        return this.salary;
    }
    // 2. Setter with validation
    setSalary(value) {
        if (value > 0) {
            this.salary = value;
        }
        else {
            console.log("Salary must be greater than 0");
        }
    }
    // 3. Method
    displayDetails() {
        console.log(`Employee ID: ${this.id}`);
        console.log(`Name: ${this.name}`);
        console.log(`Salary: ${this.salary}`);
    }
}
// 4. Derived Class: Manager
class Manager extends Employee {
    teamSize;
    // Constructor using super
    constructor(id, name, salary, teamSize) {
        super(id, name, salary);
        this.teamSize = teamSize;
    }
    // 5. Method Overriding
    displayDetails() {
        super.displayDetails(); // call base method
        console.log(`Team Size: ${this.teamSize}`);
    }
}
// 6. Object Creation
// Employee object
const emp = new Employee(1, "Geetha", 30000);
emp.displayDetails();
console.log("Updated Salary:");
emp.setSalary(35000);
console.log(emp.getSalary());
// Manager object
const mgr = new Manager(2, "Ravi", 50000, 5);
mgr.displayDetails();
export {};
