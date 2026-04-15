using System;

namespace OOps
{
    class Person
    {
        string name;
        int age;
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        public void Introduce()
        {
            Console.WriteLine($"Hi This is {name} and my age is {age}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Alice",20);
            Person p2 = new Person("Bob", 21);
            Person p3 = new Person("Charlie", 18);
            p1.Introduce();
            p2.Introduce();
            p3.Introduce();
        }
    }
    
}
