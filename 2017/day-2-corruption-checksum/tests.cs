using System;
using System.Linq;
using NUnit.Framework;
using System.IO;

namespace solution
{
    public class game_one_tests
    {
        readonly Func<string, int> subject = ChecksumCalculator.CalculateByDifference;

        [TestCase("8\t1", 7)]
        [TestCase("1\t4", 3)]
        [TestCase("7\t5\t3", 4)]
        public void should_return_expected_difference_as_checksum(string input, int expected)
        {
            var actual = subject(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void harness()
        {
            var result = File.ReadAllLines(@"c:\temp\input.txt")
                .Sum(subject);

            Console.WriteLine(result);
        }
    }

    public class game_two_tests
    {
        readonly Func<string, int> subject = ChecksumCalculator.CalculateByQuotient;

        [TestCase("8\t2", 4)]
        [TestCase("8\t4", 2)]
        [TestCase("9\t2\t3", 3)]
        public void should_return_expected_quotient_as_checksum(string input, int expected)
        {
            var actual = subject(input);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void harness()
        {
            var result = File.ReadAllLines(@"c:\temp\input.txt")
                .Sum(subject);

            Console.WriteLine(result);
        }
    }

    class ChecksumCalculator
    {
        public static int CalculateByQuotient(string input)
        {
            var total = 0;

            var integers = input.Split('\t').Select(int.Parse).ToArray();

            foreach (var value in integers)
            {
                foreach (var compareTo in integers)
                {
                    if (value != compareTo && value % compareTo == 0)
                    {
                        total += value / compareTo;
                    }
                }
            }

            return total;
        }

        public static int CalculateByDifference(string input) => FindMaximum(input) - FindMinimum(input);

        static int FindMinimum(string input) => input.Split('\t').Select(int.Parse).Min();

        static int FindMaximum(string input) => input.Split('\t').Select(int.Parse).Max();
    }
}