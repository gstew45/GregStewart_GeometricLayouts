using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("GeometricLayouts.Tests")]
namespace GeometricLayouts.Core.Models
{
    
    public class Triangle
    {
        public int Row { get; set; }

        public int Column { get; set; }

        [NotMapped]
        public List<Point> Points
        {
            get { return _Points == null ? null : JsonConvert.DeserializeObject<List<Point>>(_Points); }
            set { _Points = JsonConvert.SerializeObject(value); }
        }

        internal string _Points { get; set; }
    }
}
