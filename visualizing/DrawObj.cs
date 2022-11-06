using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace visualizing
{

    interface Drawable
    {
        void Draw(Graphics g);
        void Next();
        bool IsActive();
    }

    internal class ObjList: Drawable
    {
        List<Drawable> objs;

        public ObjList()
        {
            objs = new List<Drawable>();
        }

        public void Next()
        {
            foreach (var obj in objs)
            {
                obj.Next();
            }
        }

        public void Draw(Graphics g)
        {
            foreach(var obj in objs)
            {
                if (obj.IsActive())
                    obj.Draw(g);
            }
        }
        
        public bool IsActive()
        {
            return objs.Count > 0;
        }

        public void Add(Drawable obj)
        {
            objs.Add(obj);
        }

        public List<Drawable> GetRaw()
        {
            return objs;
        }
    }

    internal class Bar : Drawable
    {
        public int x { get; set; }
        public int y { get; set; }
        readonly float xanchor = 0, yanchor = 0;
        readonly int width, height;
        Brush brush;
        public Bar(int w, int h)
        {
            width = w;
            height = h;
            brush = Brushes.Black;
        }

        public Bar(int x, int y, int w, int h, float xa, float ya, Brush b)
        {
            (this.x, this.y) = (x, y);
            (xanchor, yanchor) = (xa, ya);
            width = w;
            height = h;
            brush = b;
        }
        public void SetPos(int x, int y)
        {
            (this.x, this.y) = (x, y);
        }
        
        public void SetBrush(Brush brush)
        {
            this.brush = brush;
        }
        public void Draw(Graphics g)
        {
            g.FillRectangle(brush, x - width * xanchor, y - height * yanchor, width, height);
        }

        public void Next()
        {

        }

        public bool IsActive()
        {
            return true;
        }
    }
}
