namespace GeometricLayouts.Core.Data
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using GeometricLayouts.Core.Models;

    public static class TriangleInitializer
    {
        public static void Initialize(TriangleContext context)
        {
            if (context.Triangles.Any())
            {
                return;
            }
            
            int amountOfRows = 60 / 10;
            int numberOfTrianglesPerCell = 2;
            int amountOfColumns = 60 / 10;

            List<Triangle> triangles = new List<Triangle>(amountOfColumns * amountOfRows * numberOfTrianglesPerCell);

            for (int row = 0; row < amountOfRows; row++)
            {
                int triangleColumn = 1;

                // Create Triangle objects with a point on this grid line.
                for (int column = 0; column < amountOfColumns; column++)
                {
                    Point topLeftCell = new Point(column, row);
                    Point topRightCell = new Point(column + 1, row);
                    Point bottomLeftCell = new Point(column, row + 1);
                    Point bottomRightCell = new Point(column + 1, row + 1);

                    Triangle firstTriangleInCell = new Triangle
                    {
                        Row = row,
                        Column = triangleColumn,
                        Points = new List<System.Drawing.Point>() {
                                                          topLeftCell,
                                                          bottomLeftCell,
                                                          bottomRightCell }
                    };

                    Triangle secondTriangleInCell = new Triangle
                    {
                        Row = row,
                        Column = triangleColumn + 1,
                        Points = new List<System.Drawing.Point>() {
                                                          topLeftCell,
                                                          topRightCell,
                                                          bottomRightCell }
                    };
                    
                    triangles.Add(firstTriangleInCell);
                    triangles.Add(secondTriangleInCell);

                    triangleColumn = triangleColumn + numberOfTrianglesPerCell;
                }
            }

            context.Triangles.AddRange(triangles);
            context.SaveChanges();
        }
    }
}
