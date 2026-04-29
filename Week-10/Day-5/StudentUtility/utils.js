"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.formatName = formatName;
exports.calculateAverage = calculateAverage;
function formatName(name) {
    return name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
}
function calculateAverage(students) {
    const total = students.reduce((sum, student) => sum + student.marks, 0);
    return total / students.length;
}
