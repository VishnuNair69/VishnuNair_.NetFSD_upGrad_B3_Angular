"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getGrade = getGrade;
exports.getTopper = getTopper;
function getGrade(marks) {
    if (marks >= 90)
        return "A";
    else if (marks >= 75)
        return "B";
    else if (marks >= 50)
        return "C";
    else
        return "F";
}
function getTopper(students) {
    return students.reduce((topper, student) => student.marks > topper.marks ? student : topper);
}
