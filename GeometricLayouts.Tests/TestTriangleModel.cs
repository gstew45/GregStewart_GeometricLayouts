namespace GeometricLayouts.Tests
{
    using GeometricLayouts.Core.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    [TestClass]
    public class TestTriangleModel
    {
        [TestMethod]
        public void TriangleCreation_PointsShouldBeNullOnCreation()
        {
            int row = 2;
            int column = 1;
            var triangle = new Triangle { Row = row, Column = column };

            Assert.AreEqual(row, triangle.Row);
            Assert.AreEqual(column, triangle.Column);

            Assert.IsNull(triangle.Points, "Triangle points is not null before points are added.");
            Assert.IsTrue(string.IsNullOrEmpty(triangle._Points));
        }

        [TestMethod]
        public void TriangleCreation_PointSerializationOnInstantiation()
        {
            int row = 2;
            int column = 1;
            var triangle = new Triangle { Row = row, Column = column };

            triangle.Points = new List<Point>() { new Point(column, row),
                                                  new Point(column, row + 1),
                                                  new Point(column + 1, row)
                                                };

            Assert.AreEqual("[\"1, 2\",\"1, 3\",\"2, 2\"]", triangle._Points);
        }

        [TestMethod]
        public void TriangleCreation_PointDeserialization()
        {
            int row = 2;
            int column = 1;
            var triangle = new Triangle { Row = row, Column = column };

            triangle.Points = new List<Point>() { new Point(column, row),
                                                  new Point(column, row + 1),
                                                  new Point(column + 1, row)
                                                };

            var deserializePoints = JsonConvert.DeserializeObject<List<Point>>("[\"1, 2\",\"1, 3\",\"2, 2\"]");
            bool areListsSequentiallyEqual = triangle.Points.SequenceEqual(deserializePoints);

            Assert.AreEqual(deserializePoints.Count, triangle.Points.Count);
            Assert.IsTrue(areListsSequentiallyEqual, "The lists are not equal.");
        }
    }
}
