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
        qsort,
        msort,
        none,
    }
    public partial class Video : Form
    {
        private VideoType type;
        //private Drawable draws;
        public static int frame = 0;
        public Graphics g;
        private Bitmap canvas;
        Debug debugform = null;
        private bool init = false;
        private List<Bar> olist;
        /* 下位15ビットは対象Barのインデックス
         * 最上位ビットが1なら赤に変化
         * 第30ビットが1なら黒に変化
         * それ以外は第29~15ビットが示すインデックスと交換
         */
        private Queue<uint> eventqueue = new Queue<uint>();

        public Video()
        {
            InitializeComponent();
            type = VideoType.none;
        }

        public Video(string type)
        {
            InitializeComponent();
            if (type == "qsort")
            {
                this.type = VideoType.qsort;
            }
            else if (type == "msort")
            {
                this.type = VideoType.msort;
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
                //draws = list;
            }
            else if (type == VideoType.qsort)
            {
                olist = new List<Bar>();
                int size = 128;
                var (w, h) = (canvas.Width, canvas.Height);
                (int min, int max) = (0, 500);
                int[] array = new int[size];
                Random random = new Random();
                for (int i = 0; i < size; ++i)
                {
                    array[i] = random.Next(min, max);
                    var (barw, barh) = ((float)w / size, (float)h / max);
                    olist.Add(new Bar((int)(barw * i), h, (int)barw, (int)(barh * array[i]), 0, 1.0f, Brushes.DarkGreen));
                }
                eventqueue = new Queue<uint>();
                quicksort(array, 0, size);
            }
        }

        private void quicksort(int[] array, int l, int r)
        {
            if (r - l <= 1)
            {
                return;
            }
            int pivot = array[l];
            eventqueue.Enqueue((uint)(0x80000000 | l));
            (int a, int b) = (l, r);
            while (a < b)
            {
                while (a < r - 1 && array[++a] < pivot) ;
                while (b > l && array[--b] > pivot) ;
                if (a >= b) break;
                (array[a], array[b]) = (array[b], array[a]);
                eventqueue.Enqueue((uint)(a | (b << 15)));
            }
            eventqueue.Enqueue((uint)(0x40000000 | l));
            eventqueue.Enqueue((uint)(l | (b << 15)));
            array[l] = array[b];
            array[b] = pivot;
            quicksort(array, l, b);
            quicksort(array, b + 1, r);
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
            if (frame % 2 == 0 && eventqueue.Count() != 0)
            {
                var ev = eventqueue.Dequeue();
                if ((ev & 0x80000000) != 0)
                {
                    olist[(int)(ev & 0x00007FFF)].SetBrush(Brushes.Blue);
                } 
                else if ((ev & 0x40000000) != 0)
                {
                    olist[(int)(ev & 0x00007FFF)].SetBrush(Brushes.DarkGreen);
                } 
                else
                {
                    int i = (int)(ev & 0x00007FFF), j = (int)(ev >> 15);
                    (olist[i].x, olist[j].x) = (olist[j].x, olist[i].x);
                    (olist[i].y, olist[j].y) = (olist[j].y, olist[i].y);
                    (olist[i], olist[j]) = (olist[j], olist[i]);
                }
            }
            foreach(var o in olist)
            {
                o.Draw(g);
            }
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
