using System;
using System.Collections.Generic;
using System.Linq;

namespace task_four
{
    class Student
    {
        public String name {get; set;}
        public int age{get; set;}
        public double grade{get; set;}
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students= new List<Student>()
            {
                new Student { name = "Arun", grade = 85.5, age = 20 },
                new Student { name = "Bala", grade = 72.3, age = 21 },
                new Student { name = "Charan", grade = 90.1,age = 19 },
                new Student { name = "Divya",grade = 88.0,age = 22 },
                new Student { name = "Esha", grade = 65.4, age = 20 }
            };
            double threshold = 80;
            var filteredStudents = students.Where(s => s.grade>=threshold).OrderBy(s => s.name);
             Console.WriteLine("Students with Grade above " + threshold + ":\n");

            foreach (var student in filteredStudents)
            {
                Console.WriteLine($"Name: {student.name}, Grade: {student.grade}, Age: {student.age}");
            }
        }
    }
}