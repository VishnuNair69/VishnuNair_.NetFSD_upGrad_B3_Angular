import { Student } from "./student.model";
import { getGrade, getTopper } from "./student.service";
import { formatName, calculateAverage } from "./utils";
import { PASS_MARKS } from "./constants";

const students: Student[] = [
  { id: 1, name: "Geetha", marks: 95 },
  { id: 2, name: "Chandhu", marks: 78 },
  { id: 3, name: "Pooji", marks: 65 }
];

console.log("Pass Marks:", PASS_MARKS);

students.forEach(student => {
  console.log(
    `Name: ${formatName(student.name)}, Grade: ${getGrade(student.marks)}`
  );
});

console.log("Average:", calculateAverage(students));
console.log("Topper:", getTopper(students));