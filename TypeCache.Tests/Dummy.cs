using System;

namespace ProProxy_Core.Tests
{
    public interface IDummy
    {
        string Name { get; set; }
        int Age { get; set; }
        DateTime DateOfBirth { get; set; }
        void Speak(string words);
        string Listen(string words);
        int Calculate(int a, int b, int c);
        DateTime WhatIsTomorrow();
    }

    public class Dummy : IDummy
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }

        public void Speak(string words)
        {
            Console.WriteLine(words.ToUpper());
        }

        public string Listen(string words)
        {
            return words.ToLower();
        }

        public int Calculate(int a, int b, int c)
        {
            return a * b * c;
        }

        public DateTime WhatIsTomorrow()
        {
            return DateTime.Now.AddDays(1);
        }
    }
}