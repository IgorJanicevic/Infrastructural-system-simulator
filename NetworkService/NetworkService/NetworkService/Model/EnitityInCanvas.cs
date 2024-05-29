using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetworkService.Model
{
    public class EnitityInCanvas
    {
        public Entity Entity { get; set; }
        public Canvas Canvas { get; set; }

        public bool IsEmpty { get; set; }

        public EnitityInCanvas(Entity entity, Canvas canvas, bool isEmpty)
        {
            Entity = entity;
            Canvas = canvas;
            IsEmpty = isEmpty;
        }

        public EnitityInCanvas()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is EnitityInCanvas canvas &&
                   EqualityComparer<Canvas>.Default.Equals(Canvas, canvas.Canvas);
        }
    }
}
