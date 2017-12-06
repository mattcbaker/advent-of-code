using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace solution
{
    public class DirectionFinderTests
    {
        Func<List<Point>, Direction> subject = DirectionFinder.FindNextDirection;

        [Test]
        public void should_return_right_when_the_spiral_is_one_square()
        {
            var spiral = new List<Point>() { new Point { x = 0, y = 0, value = 1 } };
            var actual = subject(spiral);

            Assert.AreEqual(Direction.Right, actual);
        }

        [Test]
        public void should_return_up_when_the_spiral_is_two_squares()
        {
            var spiral = new List<Point>() { new Point { x = 0, y = 0, value = 1 }, new Point { x = 0, y = 1, value = 1 } };
            var actual = subject(spiral);

            Assert.AreEqual(Direction.Up, actual);
        }


        [Test]
        public void should_return_left_when_the_spiral_is_three_squares()
        {
            var spiral = new List<Point>()
            {
                new Point { x = 0, y = 0, value = 1 },
                new Point { x = 0, y = 1, value = 1 },
                new Point { x = 1, y = 1, value = 2 }
            };
            var actual = subject(spiral);

            Assert.AreEqual(Direction.Left, actual);
        }

        [Test]
        public void should_return_left_when_the_spiral_is_four_squares()
        {
            var spiral = new List<Point>()
            {
                new Point { x = 0, y = 0, value = 1 },
                new Point { x = 0, y = 1, value = 1 },
                new Point { x = 1, y = 1, value = 2 },
                new Point { x = 1, y = 0, value = 4 },
            };
            var actual = subject(spiral);

            Assert.AreEqual(Direction.Left, actual);
        }

        [Test]
        public void should_return_down_when_the_spiral_is_five_squares()
        {
            var spiral = new List<Point>()
            {
                new Point { x = 0, y = 0, value = 1 },
                new Point { x = 0, y = 1, value = 1 },
                new Point { x = 1, y = 1, value = 2 },
                new Point { x = 1, y = 0, value = 4 },
                new Point { x = 1, y = -1, value = 5 },
            };
            var actual = subject(spiral);

            Assert.AreEqual(Direction.Down, actual);
        }
    }

    public class Point
    {
        public int x;
        public int y;
        public int value;
    }

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }

    public class DirectionFinder
    {
        public static Direction FindNextDirection(List<Point> spiral)
        {
            var last = spiral.Last();

            if (HasValueToToTheRight(spiral, last) && !HasValueBelow(spiral, last))
            {
                return Direction.Down;
            }

            if (HasValueBelow(spiral, last))
            {
                return Direction.Left;
            }

            if (HasValueToTheLeft(spiral, last))
            {
                return Direction.Up;
            }

            return Direction.Right;
        }

        static bool HasValueToToTheRight(List<Point> spiral, Point point) => 
            spiral.Exists(item => item.x == point.x && item.y == point.y + 1);

        static bool HasValueBelow(List<Point> spiral, Point point) => 
            spiral.Exists(item => item.x == point.x - 1 && item.y == point.y);

        static bool HasValueToTheLeft(List<Point> spiral, Point point) => 
            spiral.Exists(item => item.x == point.x && item.y == point.y - 1);
    }
}
