
  // Student class
  class Student {
    constructor(name, rollNumber, marks) {
      this.name = name;
      this.rollNumber = rollNumber;
      this.marks = marks;
    }

    getDetails() {
      return "Name: " + this.name + 
             ", Roll No: " + this.rollNumber + 
             ", Marks: " + this.marks;
    }

    getGrade() {
      if (this.marks >= 90) {
        return "A";
      } else if (this.marks >= 75) {
        return "B";
      } else if (this.marks >= 50) {
        return "C";
      } else {
        return "Fail";
      }
    }
  }

  
  var s1 = new Student("Vishnu", 101, 92);
  var s2 = new Student("Anita", 102, 68);

console.log(s1.getDetails());
console.log("Grade:", s1.getGrade());

console.log(s2.getDetails());
console.log("Grade:", s2.getGrade());
  
