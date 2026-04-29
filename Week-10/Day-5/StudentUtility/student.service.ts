import { Student } from "./student.model";

export function getGrade(marks: number): string {
  if (marks >= 90) return "A";
  else if (marks >= 75) return "B";
  else if (marks >= 50) return "C";
  else return "F";
}

export function getTopper(students: Student[]): Student {
  return students.reduce((topper, student) =>
    student.marks > topper.marks ? student : topper
  );
}