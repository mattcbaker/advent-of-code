using System;
using System.Linq;

namespace day_2_challenge_1
{
  class Program
  {
    static void Main(string[] args)
    {
      FirstChallenge();
      SecondChallenge();
    }

    static void FirstChallenge()
    {
      var stream = new System.IO.StreamReader(@"./input.txt");

      int @double = 0;
      int triple = 0;
      string package;
      while ((package = stream.ReadLine()) != null)
      {
        var grouped = package.GroupBy(x => x);

        if (grouped.Any(group => group.Count() == 2))
        {
          @double++;
        }

        if (grouped.Any(group => group.Count() == 3))
        {
          triple++;
        }
      }

      Console.WriteLine($"first challenge: {@double * triple}");
    }

    static void SecondChallenge()
    {
      var packages = System.IO.File.ReadAllLines(@"./input.txt");

      foreach (var package in packages)
      {
        foreach (var candidate in packages)
        {
          if (DifferingCharactersCount(package, candidate) == 1)
          {
            Console.WriteLine($"second challenge: {CommonCharacters(package, candidate)}");
            return;
          }
        }
      }
    }

    static string CommonCharacters(string first, string second) =>
      new string(first.Where((c, index) => c == second[index]).ToArray());

    static int DifferingCharactersCount(string first, string second) => 
      first.Select((c, index) => (c == second[index]) ? 0 : 1).Sum();
  }
}
