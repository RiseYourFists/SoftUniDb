using System;
using System.Collections.Generic;
using System.Linq;

namespace AvgStudentsGrades
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var inputs = int.Parse(Console.ReadLine());
            var students = new Dictionary<string, List<decimal>>();

            for (int i = 0; i < inputs; i++)
            {
                var input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var studentName = input[0];
                var grade = decimal.Parse(input[1]);

                if (students.ContainsKey(studentName))
                {
                    students[studentName].Add(grade);
                    continue;
                }

                students.Add(studentName, new List<decimal>());
                students[studentName].Add(grade);
            }

            foreach (var student in students)
            {
                Console.Write($"{student.Key} -> ");

                foreach (var grade in student.Value)
                {
                    Console.Write($"{grade:f2} ");
                }

                Console.WriteLine($"(avg: {student.Value.Average():f2})");
            }
        }
    }
}
