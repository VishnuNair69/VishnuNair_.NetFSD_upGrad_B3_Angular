using System;

class Student
{
    
    public double CalculateAverage(int m1, int m2, int m3)
    {
        double avg = (m1 + m2 + m3) / 3.0;
        return avg;
    }

    
    public void DisplayGrade(double avg)
    {
        if (avg >= 80)
            Console.WriteLine("Grade = A");
        else if (avg >= 60)
            Console.WriteLine("Grade = B");
        else if (avg >= 40)
            Console.WriteLine("Grade = C");
        else
            Console.WriteLine("Grade = Fail");
    }
}

class Program
{
    static void Main()
    {
        int m1, m2, m3;

        Console.Write("Enter marks for 3 subjects: ");
        m1 = Convert.ToInt32(Console.ReadLine());
        m2 = Convert.ToInt32(Console.ReadLine());
        m3 = Convert.ToInt32(Console.ReadLine());

       
        Student s = new Student();

        
        double avg = s.CalculateAverage(m1, m2, m3);

        Console.WriteLine("Average = " + avg);

        
        s.DisplayGrade(avg);
    }
}