namespace GeometricLayouts.Tests
{
    using GeometricLayouts.Controllers;
    using GeometricLayouts.Core.Data;
    using GeometricLayouts.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class TestTriangleController
    {
        [TestMethod]
        public void GetTriangleByRowAndColumn_ShouldReturnCorrectTriangle()
        {
            var testTriangles = GetContextWithData().Triangles;
            var controller = new TriangleController(GetContextWithData());
            int row = 2;
            int column = 12;

            var result = controller.GetByRowAndColumn(row, column);

            Assert.IsNotNull(result);
            Assert.AreEqual(row, result.Value.Row);
            Assert.AreEqual(column, result.Value.Column);
            Assert.AreEqual(3, result.Value.Points.Count());
        }

        [TestMethod]
        public void GetTriangleByRowAndColumn_ShouldNotFindTriangle()
        {
            var testTriangles = GetContextWithData().Triangles;
            var controller = new TriangleController(GetContextWithData());
            int row = 15;
            int column = 15;

            var result = controller.GetByRowAndColumn(row, column);
            var lol = result.Result;
            Assert.IsNull(result.Value, "The result was not null and a triangle was found.");
        }

        [TestMethod]
        public void GetTriangleByPoints_ShouldReturnCorrectTriangle()
        {
            var testTriangles = GetContextWithData().Triangles;
            var controller = new TriangleController(GetContextWithData());

            var testTriangle = testTriangles.First();
            List<string> points = new List<string>();

            foreach (var point in testTriangle.Points)
            {
                points.Add($"{point.X},{point.Y}");
            }

            var result = controller.GetByPoints(points.ToArray());

            Assert.IsNotNull(result);
            
            for (int i = 0; i < result.Value.Points.Count; i++)
            {
                Assert.AreEqual(points[i], $"{result.Value.Points[i].X},{result.Value.Points[i].Y}");
            }
        }

        [TestMethod]
        public void GetTriangleByPoints_ShouldNotFindTriangle()
        {
            var controller = new TriangleController(GetContextWithData());

            List<string> points = new List<string>();
            points.Add("2,7");
            points.Add("5,2");
            points.Add("1,2");

            var result = controller.GetByPoints(points.ToArray());

            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void GetTriangleByPoints_ShouldNotFindSearchingWithAllSameCoordinates()
        {
            var controller = new TriangleController(GetContextWithData());

            List<string> points = new List<string>();
            points.Add("3,3");
            points.Add("3,3");
            points.Add("3,3");

            var result = controller.GetByPoints(points.ToArray());

            Assert.IsNull(result.Value);
        }

        private TriangleContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<TriangleContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new TriangleContext(options);

            TriangleInitializer.Initialize(context);

            return context;
        }
    }
}
