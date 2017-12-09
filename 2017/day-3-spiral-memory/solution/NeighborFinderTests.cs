using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace solution
{
    public class NeighborFinderTests
    {
        Func<List<Point>, int, int, int> subjectNew = NeighborFinder.SumValueOfNeighbors;

        [Test]
        public void should_return_0_when_there_are_no_neighbors()
        {
            var point = new Point { x = 0, y = 0, value = 1 };
            var spiral = new List<Point>() { new Point { x = 0, y = 0, value = 1 } };
            var actual = subjectNew(spiral, point.x, point.y);


            Assert.AreEqual(0, actual);
        }

        [Test]
        public void should_return_value_of_neighbor_if_there_is_one_neighbor()
        {
            var point = new Point { x = 0, y = 0, value = 1 };

            var spiral = new List<Point>()
            {
                point,
                new Point { x = 0, y = 1, value = 1 },
            };

            var actual = subjectNew(spiral, point.x, point.y);

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void should_return_value_of_multiple_neighbors()
        {
            var point = new Point { x = 0, y = 0, value = 1 };

            var spiral = new List<Point>()
            {
                point,
                new Point { x = 1, y = -1, value = 3 },
                new Point { x = 1, y = 0, value = 2 },
                new Point { x = 1, y = 1, value = 1 },
                new Point { x = 0, y = -1, value = 1 },
                new Point { x = 0, y = + 1, value = 1 },
                new Point { x = -1, y = -1, value = 1 },
                new Point { x = -1, y = 0, value = 1 },
                new Point { x = -1, y = + 1, value = 1 },
            };
            var actual = subjectNew(spiral, point.x, point.y);

            Assert.AreEqual(11, actual);
        }
    }

    public class NeighborFinder
    {
        public static int SumValueOfNeighbors(List<Point> spiral, int x, int y)
        {
            var sum = 0;

            sum += GetValueForPointSafe(spiral, x + 1, y - 1);
            sum += GetValueForPointSafe(spiral, x + 1, y);
            sum += GetValueForPointSafe(spiral, x + 1, y + 1);

            sum += GetValueForPointSafe(spiral, x, y - 1);
            sum += GetValueForPointSafe(spiral, x, y + 1);


            sum += GetValueForPointSafe(spiral, x - 1, y - 1);
            sum += GetValueForPointSafe(spiral, x - 1, y);
            sum += GetValueForPointSafe(spiral, x - 1, y + 1);

            return sum;
        }

        static bool PointExistsAt(List<Point> spiral, int x, int y) =>
            spiral.Exists(candidate => candidate.x == x && candidate.y == y);

        static int GetValueForPointSafe(List<Point> spiral, int x, int y) =>
            PointExistsAt(spiral, x, y)
            ? spiral.First(candidate => candidate.x == x && candidate.y == y).value
            : 0;
    }
}