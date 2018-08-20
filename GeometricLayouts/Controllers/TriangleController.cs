namespace GeometricLayouts.Controllers
{
    using GeometricLayouts.Core.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    [Route("api/triangle")]
    [ApiController]
    public class TriangleController : ControllerBase
    {
        private readonly TriangleContext _context;

        public TriangleController(TriangleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<Triangle> GetByRowAndColumn([FromQuery]int row, [FromQuery]int column)
        {
            var item = _context.Triangles.Find(row, column);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public ActionResult<Triangle> GetByPoints([FromBody]string[] points)
        {
            foreach (Triangle triangle in _context.Triangles.ToList())
            {
                List<Point> pointsBeingQueryed = new List<Point>();
                foreach (string point in points)
                {
                    string[] coords = point.Split(',');
                    pointsBeingQueryed.Add(new Point(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1])));
                }

                if (triangle.Points.SequenceEqual(pointsBeingQueryed))
                {
                    return triangle;
                }
            }

            return NotFound();
        }
    }
}
