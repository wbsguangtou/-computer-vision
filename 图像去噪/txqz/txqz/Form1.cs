using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace txqz
{
    public partial class Form1 : Form
    {
        private string fileName;
        Bitmap bt1;
        Bitmap bt2;
        public Form1()
        {
            InitializeComponent();
        }

        public string getFileName()
        {
            return fileName;
        }

        public void setFilename(string name)
        {
            fileName = name;
        }
        private void button2_Click(object sender, EventArgs e)//打开图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                setFilename(openFileDialog1.FileName);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
            }
        }

        private void button1_Click(object sender, EventArgs e)//加随机噪声
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image);
            Random random = new Random();
            for (int i = 0; i < bt1.Width; i++)
            {
                for (int j = 0; j < bt1.Height; j++)
                {
                    int R, G, B;
                    double ran1 = random.NextDouble();//获得0-1的随机数
                    int a = random.Next(0, 30);
                    int b = random.Next(0, 30);
                    int c = random.Next(0, 30);
                    R = bt1.GetPixel(i, j).R;
                    G = bt1.GetPixel(i, j).G;
                    B = bt1.GetPixel(i, j).B;
                    if (ran1 > 0.85)//如果随机数大于0.85则将该点设为随机色值
                    {
                        R = 255 - a;
                        G = 255 - b;
                        B = 255 - c;
                    }
                    if (ran1 < 0.15)//如果随机数小于0.15则将该点设为随机色值
                    {
                        R = a;
                        G = b;
                        B = c;
                    }
                    bt2.SetPixel(i, j, Color.FromArgb(R, G, B));
                }
                pictureBox2.Refresh();
                pictureBox2.Image = bt2;
            }
        }

        private void button3_Click(object sender, EventArgs e)//最大值滤波
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            Color color = new Color();
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image);
            for(int i = 1; i < bt1.Width - 1; i++)
            {
                for(int j = 1; j < bt1.Height - 1; j++)
                {
                    int rm = 0, r1, gm = 0, g1, bm = 0, b1;
                    for(int m = 0;m < 3; m++)
                    {
                        for(int n = 0;n < 3; n++)
                        {
                            color = bt1.GetPixel(i + m - 1, j + n - 1);
                            r1 = color.R;
                            if (r1 > rm)
                                rm = r1;
                            g1 = color.G;
                            if(g1 > gm)
                                gm = g1;
                            b1 = color.B;
                            if(b1 > bm)
                                bm = b1;
                        }
                    }
                    bt2.SetPixel(i,j,Color.FromArgb(rm,gm,bm));
                }
                pictureBox3.Refresh();
                pictureBox3.Image = bt2;
            }
        }

        private void button4_Click(object sender, EventArgs e)//加黑白噪声
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image);
            Random random = new Random();
            double Pa = 0.1;
            double Pb = 0.1;
            double P = Pb / (1 - Pa);
            for (int i = 0; i < bt1.Height; i++)
            {
                for (int j = 0; j < bt1.Width; j++)
                {
                    int gray;
                    int noise = 1;
                    double ran1 = random.NextDouble();
                    if (ran1 < Pa)
                    {
                        noise = 255;
                    }
                    else
                    {
                        double ran2 = random.NextDouble();
                        if (ran2 < P)
                            noise = 0;
                    }
                    if (noise != 1)
                    {
                        gray = noise;
                    }
                    else
                        gray = bt1.GetPixel(j, i).R;
                    Color color = Color.FromArgb(gray, gray, gray);
                    bt2.SetPixel(j, i, color);
                }
            }
            pictureBox2.Refresh();
            pictureBox2.Image = bt2;
        }

        private void button5_Click(object sender, EventArgs e)//四邻域平均
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image); //定义两个位图对象
            int R1, R2, R3, R4, Red;
            int G1, G2, G3, G4, Green;
            int B1, B2, B3, B4, Blue;
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    R1 = bt1.GetPixel(i, j - 1).R;
                    R2 = bt1.GetPixel(i, j + 1).R;
                    R3 = bt1.GetPixel(i - 1, j).R;
                    R4 = bt1.GetPixel(i + 1, j).R;
                    Red = (R1 + R2 + R3 + R4) / 5;
                    G1 = bt1.GetPixel(i, j - 1).G;
                    G2 = bt1.GetPixel(i, j + 1).G;
                    G3 = bt1.GetPixel(i - 1, j).G;
                    G4 = bt1.GetPixel(i + 1, j).G;
                    Green = (G1 + G2 + G3 + G4) / 5;
                    B1 = bt1.GetPixel(i, j - 1).B;
                    B2 = bt1.GetPixel(i, j + 1).B;
                    B3 = bt1.GetPixel(i - 1, j).B;
                    B4 = bt1.GetPixel(i + 1, j).B;
                    Blue = (B1 + B2 + B3 + B4) / 5;
                    bt2.SetPixel(i, j, Color.FromArgb(Red, Green, Blue));
                }
                pictureBox3.Refresh();//刷新图片框
                pictureBox3.Image = bt2;
            }
        }

        private void button6_Click(object sender, EventArgs e)//八邻域平均
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image); //定义两个位图对象
            int R1, R2, R3, R4, R5, R6, R7, R8, Red;
            int G1, G2, G3, G4, G5, G6, G7, G8, Green;
            int B1, B2, B3, B4, B5, B6, B7, B8, Blue;
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    R1 = bt1.GetPixel(i - 1, j - 1).R;
                    R2 = bt1.GetPixel(i, j - 1).R;
                    R3 = bt1.GetPixel(i + 1, j - 1).R;
                    R4 = bt1.GetPixel(i - 1, j).R;
                    R5 = bt1.GetPixel(i + 1, j).R;
                    R6 = bt1.GetPixel(i - 1, j + 1).R;
                    R7 = bt1.GetPixel(i, j + 1).R;
                    R8 = bt1.GetPixel(i + 1, j + 1).R;
                    Red = (int)(R1 + R2 + R3 + R4 + R5 + R6 + R7 + R8) / 8;
                    G1 = bt1.GetPixel(i - 1, j - 1).G;
                    G2 = bt1.GetPixel(i, j - 1).G;
                    G3 = bt1.GetPixel(i + 1, j - 1).G;
                    G4 = bt1.GetPixel(i - 1, j).G;
                    G5 = bt1.GetPixel(i + 1, j).G;
                    G6 = bt1.GetPixel(i - 1, j + 1).G;
                    G7 = bt1.GetPixel(i, j + 1).G;
                    G8 = bt1.GetPixel(i + 1, j + 1).G;
                    Green = (int)(G1 + G2 + G3 + G4 + G5 + G6 + G7 + G8) / 8;
                    B1 = bt1.GetPixel(i - 1, j - 1).B;
                    B2 = bt1.GetPixel(i, j - 1).B;
                    B3 = bt1.GetPixel(i + 1, j - 1).B;
                    B4 = bt1.GetPixel(i - 1, j).B;
                    B5 = bt1.GetPixel(i + 1, j).B;
                    B6 = bt1.GetPixel(i - 1, j + 1).B;
                    B7 = bt1.GetPixel(i, j + 1).B;
                    B8 = bt1.GetPixel(i + 1, j + 1).B;
                    Blue = (int)(B1 + B2 + B3 + B4 + B5 + B6 + B7 + B8) / 8;
                    bt2.SetPixel(i, j, Color.FromArgb(Red, Green, Blue));
                }
                pictureBox3.Refresh();//刷新图片框
                pictureBox3.Image = bt2;
            }
        }

        private void button7_Click(object sender, EventArgs e)//最小值滤波
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            Color color = new Color();
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image);
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    int rm = 255, r1, gm = 255, g1, bm = 255, b1;
                    for (int m = 0; m < 2; m++)
                    {
                        for (int n = 0; n < 2; n++)
                        {
                            color = bt1.GetPixel(i + m - 1, j + n - 1);
                            r1 = color.R;
                            if (r1 < rm)
                                rm = r1;
                            g1 = color.G;
                            if (g1 < gm)
                                gm = g1;
                            b1 = color.B;
                            if (b1 < bm)
                                bm = b1;
                        }
                    }
                    bt2.SetPixel(i, j, Color.FromArgb(rm, gm, bm));
                }
                pictureBox3.Refresh();
                pictureBox3.Image = bt2;
            }
        }

        private void button8_Click(object sender, EventArgs e)//中值滤波
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            Color c = new Color();
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image);
            int rr, r1, dm, m;
            int[] dt = new int[20];
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    rr = 0; m = 0;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int n = -1; n < 2; n++)
                        {
                            c = bt1.GetPixel(i + k, j + n);
                            r1 = c.R;
                            dt[m++] = r1;
                        }
                    }
                    for (int p = 0; p < m - 1; p++)
                    {
                        for (int q = p + 1; q < m; q++)
                        {
                            if (dt[p] > dt[q])
                            {
                                dm = dt[p];
                                dt[p] = dt[q];
                                dt[q] = dm;
                            }
                        }
                    }
                    rr = dt[(int)(m / 2)];
                    bt2.SetPixel(i, j, Color.FromArgb(rr, rr, rr));
                }
                pictureBox3.Refresh();
                pictureBox3.Image = bt2;
            }
        }

        private void button9_Click(object sender, EventArgs e)//修正平均滤波
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            else if (pictureBox2.Image == null)
            {
                MessageBox.Show("错误，没有完成加噪！");
                return;
            }
            Color c = new Color();
            bt1 = new Bitmap(pictureBox2.Image);
            bt2 = new Bitmap(pictureBox2.Image);
            int rr, r1, dm, m;
            int[] dt = new int[20];
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    rr = 0; m = 0;
                    for (int k = -1; k < 2; k++)
                    {
                        for (int n = -1; n < 2; n++)
                        {
                            c = bt1.GetPixel(i + k, j + n);
                            r1 = c.R;
                            dt[m] = r1;
                            m++;
                        }
                    }
                    for (int p = 0; p < m - 1; p++)
                    {
                        for (int q = p + 1; q < m; q++)
                        {
                            if (dt[p] > dt[q])
                            {
                            dm = dt[p];
                                dt[p] = dt[q];
                                dt[q] = dm;
                            }
                        }
                    }
                    for (int l = 1; l < m - 1; l++)
                        rr += dt[l];
                    rr = (int)(rr / (m - 2));
                    bt2.SetPixel(i, j, Color.FromArgb(rr, rr, rr));
                }
                pictureBox3.Refresh();
                pictureBox3.Image = bt2;
            }
        }
    }
}