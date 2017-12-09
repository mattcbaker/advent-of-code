using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace solution
{
    class SpiralMagicTests
    {
        Func<int, int> subject = SpiralMagic.GetNextLargestValue;

        [Test]
        public void should_return_2_for_threshold_of_1()
        {
            var actual = subject(1);

            Assert.AreEqual(2, actual);
        }

        [Test]
        public void should_return_4_for_threshold_of_2()
        {
            var actual = subject(2);

            Assert.AreEqual(4, actual);
        }

        [Test]
        public void should_return_5_for_threshold_of_4()
        {
            var actual = subject(4);

            Assert.AreEqual(5, actual);
        }

        [Test]
        public void should_return_54_for_threshold_of_27()
        {
            var actual = subject(26);

            Assert.AreEqual(54, actual);
        }

        [Test]
        public void harness()
        {
            var actual = subject(277678);

            Assert.AreEqual(279138, actual);
        }
    }

    public class SpiralMagic
    {
        public static int GetNextLargestValue(int threshold)
        {
            var spiral = new List<Point> { new Point { x = 0, y = 0, value = 1 } };

            while (spiral.Last().value <= threshold)
            {
                spiral.Add(GetNextPointForSpiral(spiral));
            }

            return spiral.Last().value;
        }

        static Point GetNextPointForSpiral(List<Point> spiral)
        {
            var direction = DirectionFinder.FindNextDirection(spiral);
            var x = spiral.Last().x;
            var y = spiral.Last().y;

            switch (direction)
            {
                case Direction.Right:
                    y++;
                    break;
                case Direction.Up:
                    x++;
                    break;
                case Direction.Left:
                    y--;
                    break;
                case Direction.Down:
                    x--;
                    break;
            }

            var value = Neighbors.SumValueOfNeighbors(spiral, x, y);

            return new Point { x = x, y = y, value = value };
        }
    }
}