namespace GeometricLayouts.Tests
{
    using GeometricLayouts.Core.Data;
    using GeometricLayouts.Core.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class TestTriangleInitializer
    {
        [TestMethod]
        public void InitializeDatabase_ShouldProduceCorrectAmountOfTriangles()
        {
            var options = new DbContextOptionsBuilder<TriangleContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new TriangleContext(options);

            Assert.AreEqual(0, context.Triangles.Count());

            TriangleInitializer.Initialize(context);

            Assert.AreEqual(72, context.Triangles.Count());
        }
    }
}
