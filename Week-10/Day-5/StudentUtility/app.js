"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const student_service_1 = require("./student.service");
const utils_1 = require("./utils");
const constants_1 = require("./constants");
const students = [
    { id: 1, name: "Geetha", marks: 95 },
    { id: 2, name: "Chandhu", marks: 78 },
    { id: 3, name: "Pooji", marks: 65 }
];
console.log("Pass Marks:", constants_1.PASS_MARKS);
students.forEach(student => {
    console.log(`Name: ${(0, utils_1.formatName)(student.name)}, Grade: ${(0, student_service_1.getGrade)(student.marks)}`);
});
console.log("Average:", (0, utils_1.calculateAverage)(students));
console.log("Topper:", (0, student_service_1.getTopper)(students));
