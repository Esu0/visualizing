using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualizing
{
    enum VideoType
    {
        stk_que,
        sort,
        none,
    }
    public partial class Video : Form
    {
        private VideoType type;
        private Drawable draws;
        public static int frame = 0;
        public Graphics g;
        private Bitmap canvas;
        Debug debugform = null;
        private bool init = false;
        public Video()
        {
            InitializeComponent();
            type = VideoType.none;
        }

        public Video(string type)
        {
            InitializeComponent();
            if (type == "sort")
            {
                this.type = VideoType.sort;
            }
            else if (type == "stk_que")
            {
                this.type = VideoType.stk_que;
            }
            else this.type = VideoType.none;
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            init_draws();
        }

        private void init_draws()
        {
            if (type == VideoType.stk_que) 
            {
                var list = new ObjList();
                var (w, h) = (canvas.Width, canvas.Height);
                list.Add(new Cup(300, h - 150 - 3, 100, 150, 500));
                list.Add(new Text("スタックの動作", new int[] { 500 }, 20, h / 3, true, 20));
                list.Add(new Text("push(100);", new int[] { 50, 120 }, 50, h / 3 + 30, false, 15));
                VObj vo = new VObj();
                (vo.x, vo.y) = (305, 0);
                vo.active = false;
                vo.update = (ref float x, ref float y, ref float vx, ref float vy, ref float e, ref float f) => {
                    if (frame > 60)
                    {
                        vy = 8f;
                    }
                    if (y >= h - 8 - 30)
                    {
                        vy = 0;
                        y = h - 8 - 30;
                    }
                };
                list.Add(new Word(new Rect(90, 30, vo.Clone() as VObj), "100", new int[] {60, 500}, 20));
                vo.delay = 120;
                list.Add(new Word(new Rect(90, 30, vo.Clone() as VObj), "100", new int[] {60, 500}, 20));
                draws = list;
            }
            else if (type == VideoType.sort)
            {
                draws = new Cup();
            }
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            frame = 0;
            if (debugform != null)
                debugform.Close();
            Close();
        }

        private void stopstart_button_Click(object sender, EventArgs e)
        {
            timer1.Enabled ^= init;
        }

        private void init_button_Click(object sender, EventArgs e)
        {
            init_draws();
            timer1.Enabled = true;
            init = true;
            frame = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(canvas);
            g.FillRectangle(Brushes.White, 0, 0, canvas.Width, canvas.Height);
            frame++;
            if (draws.IsActive())
                draws.Draw(g);
            draws.Next();
            pictureBox1.Image = canvas;
            g.Dispose();
            //canvas.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            debugform = new Debug();
            debugform.Show();
        }

        private void Video_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            frame = 0;
            if (debugform != null)
                debugform.Close();
        }
    }
}
