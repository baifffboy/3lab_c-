using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab3._2
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        List<Rectangle> list = new List<Rectangle>();
        List<Color> col = new List<Color>();
        List<System.Windows.Forms.Label> lis = new List<System.Windows.Forms.Label>();
        System.Drawing.Pen pen = new Pen(Color.Black);
        Timer timer = new Timer();
        int bas = 0;
        bool t = true;
        int a = 0;
        byte rg = 0;
        byte f = 0;
        byte b = 0;
        int r = -1;
        Color z = Color.White;
        System.Windows.Forms.Label n = new System.Windows.Forms.Label();
        public Form1()
        {
            InitializeComponent();
            list = new List<Rectangle>();
            lis = new List<System.Windows.Forms.Label>();
            col = new List<Color>();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += Timer_Tick;
            DoubleBuffered = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            bas += 1;
            if ((list.Count == 0) && (bas > 10))
            {
                timer.Stop();
                MessageBox.Show("Вы выйграли!");
                t = false;
                Application.Exit();
            }
            Create_Rectangles(1);
            if (list.Count == 20)
            {
                timer.Stop();
                MessageBox.Show("Вы проиграли!");
                t = false;
                Application.Exit();
                Form1 form = new Form1();
                form.Close();
            }
            this.Invalidate();
        }

        private void Create_Rectangles(int v)
        {
            for (int i = 0; i < v; i++)
            {
                Rectangle r = new Rectangle();
                int p = (int)random.Next(2);
                switch (p)
                {
                    case 0:
                        r.Width = 200;
                        r.Height = 100;
                        r.Location = new Point(random.Next((int)(this.Width - r.Width)), random.Next((int)(this.Height - r.Height)));
                        rg = (byte)random.Next(255);
                        f = (byte)random.Next(255);
                        b = (byte)random.Next(255);
                        z = Color.FromArgb(rg, f, b);
                        col.Add(z);
                        break;
                    case 1:
                        r.Width = 100;
                        r.Height = 200;
                        r.Location = new Point(random.Next((int)(this.Width - r.Width)), random.Next((int)(this.Height - r.Height)));
                        rg = (byte)random.Next(255);
                        f = (byte)random.Next(255);
                        b = (byte)random.Next(255);
                        z = Color.FromArgb(rg, f, b);
                        col.Add(z);
                        break;
                }
                if (t)
                {
                    list.Add(r);
                    ++a;
                    this.Paint += Form1_Paint;
                }
            }
        }

        private bool proverka_na_nalogenie(int element)
        {
            if (element + 1 != list.Count)
            {
                for (int i = element + 1; i < list.Count; i++)
                {
                    if (((list[element].Location.Y <= list[i].Location.Y + list[i].Height) && (list[element].Location.Y + list[element].Height >= list[i].Location.Y + list[i].Height)) || ((list[element].Location.Y + list[element].Height >= list[i].Location.Y) && (list[element].Location.Y + list[element].Height <= list[i].Location.Y + list[i].Height)) || ((list[element].Location.Y <= list[i].Location.Y) && (list[element].Location.Y + list[element].Height >= list[i].Location.Y + list[i].Height)))
                    {
                        if (((list[element].Location.X <= list[i].Location.X + list[i].Width) && (list[element].Location.X + list[element].Width >= list[i].Location.X + list[i].Width)) || ((list[element].Location.X + list[element].Width >= list[i].Location.X) && (list[element].Location.X + list[element].Width <= list[i].Location.X + list[i].Width)) || ((list[element].Location.X <= list[i].Location.X) && (list[element].Location.X + list[element].Width >= list[i].Location.X + list[i].Width)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return true;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick_1(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if (bas > 10)
            {
                if (proverka_na_nalogenie2(x, y) && proverka_na_nalogenie(list.IndexOf(list[r])))
                {
                    list.Remove(list[r]);
                    a--;
                }
            }
            this.Invalidate();
        }

        private bool proverka_na_nalogenie2(int x, int y)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (((0 <= list[i].Location.X + list[i].Width - x) && (list[i].Location.X + list[i].Width - x <= list[i].Width)) && ((0 <= list[i].Location.Y + list[i].Height - y) && (list[i].Location.Y + list[i].Height - y <= list[i].Height)))
                {
                    r = i;
                }
            }
            if (r != -1)
            {
                return true;
            }
            else { 
                return false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (t)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    SolidBrush br = new SolidBrush(col[i]);
                    System.Drawing.Pen pen = new Pen(Color.Black);
                    e.Graphics.DrawRectangle(pen, list[i].Location.X, list[i].Location.Y, list[i].Width, list[i].Height);
                    e.Graphics.FillRectangle(br, list[i].Location.X, list[i].Location.Y, list[i].Width, list[i].Height);
                }
                if (bas == 10)
                {
                    n = new System.Windows.Forms.Label();
                    n.Width = 175;
                    n.Height = 150;
                    e.Graphics.DrawRectangle(pen, (this.Location.X + this.Width) / 2 - 150, (this.Location.Y + this.Height) / 2 - 150, n.Width, n.Height);
                    e.Graphics.FillRectangle(Brushes.White, (this.Location.X + this.Width) / 2 - 150, (this.Location.Y + this.Height) / 2 - 150, n.Width, n.Height);
                    lis.Add(n);
                    StringFormat Sf = new StringFormat();
                    Font fn = new Font("Microsoft Sans Serif", 16);
                    e.Graphics.DrawString("Игра началась!", fn, Brushes.Black, new PointF((this.Location.X + this.Width) / 2 - 145, (this.Location.Y + this.Height) / 2 - 95), Sf);
                }
                if (bas == 11)
                {
                    lis.Remove(n);
                }
            }
        }
        
    }
}