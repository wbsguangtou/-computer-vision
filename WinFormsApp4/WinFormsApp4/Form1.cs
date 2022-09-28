using System.Drawing.Imaging;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap btmp;

        private void button1_Click(object sender, EventArgs e)//打开图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)//打开图片在picturebox组件上展示
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                btmp = new Bitmap(pictureBox1.Image);//利用Bitmap以获取打开图片个像素点
            }
        }

        private void button2_Click(object sender, EventArgs e)//图像灰度化
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            Bitmap bt = new Bitmap(pictureBox1.Image);
            Bitmap bt1 = new Bitmap(pictureBox1.Image);
            Color color = new Color();
            for (int i = 0; i < bt.Width; i++)//利用两个循环对每个像素点进行灰度化
            {
                for (int j = 0; j < bt.Height; j++)
                {
                    color = bt.GetPixel(i, j);
                    int n = (int)((color.G * 59 + color.R * 30 + color.B * 11) / 100);//对单个像素点进行灰度化处理
                    bt1.SetPixel(i, j, Color.FromArgb(n, n, n));

                }
                pictureBox1.Refresh();
                pictureBox1.Image = bt1;//将pictureBox组件中图片更换为灰度化后图片
            }

        }

        private void button3_Click(object sender, EventArgs e)//图像二值化
        {

            Color p1;
            Bitmap bt1 = btmp.Clone() as Bitmap;
            for (int x1 = 0; x1 < btmp.Width; x1++)
            {
                for (int x2 = 0; x2 < btmp.Height; x2++)
                {
                    p1 = btmp.GetPixel(x1, x2);
                    int temp = (p1.R + p1.B + p1.G) / 3;
                    bt1.SetPixel(x1, x2, Color.FromArgb(temp, temp, temp));
                }
            }

            Color p2;
            for (int x1 = 0; x1 < bt1.Width; x1++)
            {
                for (int x2 = 0; x2 < bt1.Height; x2++)
                {
                    p2 = bt1.GetPixel(x1, x2);
                    if (p2.G > 150)
                    {
                        bt1.SetPixel(x1, x2, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bt1.SetPixel(x1, x2, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            pictureBox1.Refresh();
            pictureBox1.Image = bt1;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)//调节亮度
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            int value = int.Parse(textBox1.Text);
             Bitmap bt = new Bitmap(pictureBox1.Image); 
             Bitmap bt1 = new Bitmap(pictureBox1.Image);
            int r, g, b; 
            for (int i = 0; i < bt1.Width; i++)
            {
                for (int j = 0; j < bt1.Height; j++)
                {
                    Color color = bt.GetPixel(i, j);
                    r = color.R;
                    g = color.G;
                    b = color.B;
                    r += value;
                    g += value;
                    b += value;
                    if (r > 255) 
                        r = 255;
                    if (r < 0)
                        r = 0; 
                    if (g > 255) 
                        g = 255;
                    if (g < 0) 
                        g = 0;
                    if (b > 255) 
                        b = 255;
                    if (b < 0) 
                        b = 0;
                    Color c1 = Color.FromArgb(r, g, b);
                    bt1.SetPixel(i, j, c1);
                }
                pictureBox1.Refresh();
                pictureBox1.Image = bt1;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)//对比度调节
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("错误，没有导入图片！");
                return;
            }
            int degree = int.Parse(textBox2.Text);
            int r, g, b;
            Bitmap bt = btmp;
            Bitmap bt1 = new Bitmap(pictureBox1.Image);
            for (int i = 0; i < bt1.Width; i++)
            {
                for (int j = 0; j < bt1.Height; j++)
                {
                    Color c = bt.GetPixel(i, j);
                    r = c.R;
                    g = c.G;
                    b = c.B;
                    int rg = (Math.Abs(127 - r) * degree) / 255;
                    int gg = (Math.Abs(127 - g) * degree) / 255;
                    int bg = (Math.Abs(127 - b) * degree) / 255;
                    if (r > 127) r = r + rg;
                    else r = r - rg;
                    if (g > 127) g = g + gg;
                    else g = g - gg;
                    if (b > 127) b = b + bg;
                    else b = b - bg;
                    if (r > 255) r = 255;
                    if (r < 0) r = 0;
                    if (g > 255) g = 255;
                    if (g < 0) g = 0;
                    if (b > 255) b = 255;
                    if (b < 0) b = 0;
                    Color c1 = Color.FromArgb(r, g, b);
                    bt1.SetPixel(i, j, c1);
                }
                pictureBox1.Refresh();
                pictureBox1.Image = bt1;
            }
        }
    }
    
}