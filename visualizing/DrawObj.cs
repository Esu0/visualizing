using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace visualizing
{
    delegate void Update(ref float x, ref float y, ref float vx, ref float vy, ref float ax, ref float ay);

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
    internal class Cup : Drawable
    {
        bool active = true;
        int x = 0, y = 0;
        public int height = 100, width = 100;
        int lifetime = 0;
        public Cup()
        {
        }

        public Cup(int x, int y, int w, int h, int l)
        {
            this.x = x;
            this.y = y;
            (width, height) = (w, h);
            lifetime = l;
        }

        public void Draw(Graphics g)
        {
            g.DrawLines(new Pen(Color.Black, 3), new Point[] { new Point(x, y), new Point(x, y + height), new Point(x + width, y + height), new Point(x + width, y) });
        }
        public void Next()
        {
            if (Video.frame >= lifetime)
                active = false;
        }
        public bool IsActive()
        {
            return active;
        }
    }

    internal class Text : Drawable
    {
        int x = 0, y = 0;
        
        string txt;
        int[] listToggle;
        int index = 0;
        Font font;
        bool active;
        public Text(string txt, int[] framelist, int x = 0, int y = 0, bool active = true, int pt = 11, string font = "游ゴシック")
        {
            this.txt = txt;
            listToggle = framelist;
            this.font = new Font(font, pt);
            this.active = active;
            this.x = x;
            this.y = y;
        }

        public void setPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(Graphics g)
        {
            g.DrawString(txt, font, Brushes.Black, x, y);
        }

        public void Next()
        {
            if (index < listToggle.Length && Video.frame >= listToggle[index])
            {
                index++;
                active ^= true;
            }
        }

        public bool IsActive()
        {
            return active;
        }
    }

    internal class Rect : Drawable
    {
        public VObj vobj;
        int width, height;
        public Brush brush;
        bool fill;
        public Rect(int w, int h, VObj v, Brush b, bool f = false)
        {
            (width, height, vobj, brush) = (w, h, v, b);
            fill = f;
        }
        public Rect(int w, int h, VObj v, bool f = false)
        {
            (width, height, vobj, brush) = (w, h, v, Brushes.Black);
            fill = f;
        }

        public void Draw(Graphics g)
        {
            if (fill)
            {
                g.FillRectangle(brush, vobj.x, vobj.y, (float)width, (float)height);
            }
            else
            {
                g.DrawRectangle(new Pen(brush), vobj.x, vobj.y, (float)width, (float)height);
            }
        }

        public void Next()
        {
            vobj.Next();
        }

        public bool IsActive()
        {
            return vobj.active;
        }
    }

    internal class Word : Drawable
    {
        public Rect rect;
        string txt;
        int[] listToggle;
        int index = 0;
        Font font;
        public Word(Rect r, string text, int[] list, int pt = 11, string fontname = "游ゴシック")
        {
            (rect, txt, listToggle, font) = (r, text, list, new Font(fontname, pt));
        }

        public void Draw(Graphics g)
        {
            if (Video.frame >= rect.vobj.delay)
            {
                rect.Draw(g);
                g.DrawString(txt, font, rect.brush, rect.vobj.x, rect.vobj.y);
            }
        }

        public void Next()
        {
            if (index < listToggle.Length && Video.frame >= listToggle[index])
            {
                rect.vobj.active ^= true;
                index++;
            }
            rect.Next();
        }

        public bool IsActive()
        {
            return rect.vobj.active;
        }
    }
    internal class VObj : ICloneable
    {
        public float x, y;
        public float vx, vy;
        public float ax, ay;
        public bool active;
        public int delay;
        public Update update = (ref float a, ref float b, ref float c, ref float d, ref float e, ref float f) => { return; };
        public VObj()
        {
            (x, y, vx, vy, ax, ay, delay) = (0, 0, 0, 0, 0, 0, 0);
            active = true;
        }

        public object Clone()
        {
            var ob = new VObj();
            (ob.x, ob.y, ob.vx, ob.vy, ob.ax, ob.ay, ob.delay) = (x, y, vx, vy, ax, ay, delay);
            return ob;
        }
        public void Next()
        {
            if (Video.frame >= delay)
            {
                update(ref x, ref y, ref vx, ref vy, ref ax, ref ay);
                x += vx;
                y += vy;
                vx += ax;
                vy += ay;
            }
        }
    }

    internal class Bar : Drawable
    {
        public int x { get; set; }
        public int y { get; set; }
        float xanchor = 0, yanchor = 0;
        int width, height;
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
