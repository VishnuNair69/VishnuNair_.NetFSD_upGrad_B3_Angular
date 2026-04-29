import { Student } from "./student.model";

export function formatName(name: string): string {
  return name.charAt(0).toUpperCase() + name.slice(1).toLowerCase();
}

export function calculateAverage(students: Student[]): number {
  const total = students.reduce((sum, student) => sum + student.marks, 0);
  return total / students.length;
}