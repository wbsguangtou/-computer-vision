using System.Windows.Forms;

namespace wcs
{
    public partial class Form1 : Form
    {
        Bitmap bt1;
        Bitmap bt2;
        Bitmap bt3;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)//打开图片
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
            }
        }

        private void button2_Click(object sender, EventArgs e)//图片灰度处理
        {
            if(pictureBox1.Image == null)
            {
                MessageBox.Show("未打开图片！");
                return;
            }
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image); 
            Color color = new Color();
            for (int i = 0; i < bt1.Width; i++)
            {
                for (int j = 0; j < bt1.Height; j++)
                {
                    color = bt1.GetPixel(i, j);
                    int n = (int)((color.G * 59 + color.R * 30 + color.B * 11) / 100);
                bt2.SetPixel(i, j, Color.FromArgb(n, n, n)); 

                }
                pictureBox1.Refresh();
                pictureBox1.Image = bt2;
            }
            bt3 = new Bitmap(pictureBox1.Image);
        }

        private void button3_Click(object sender, EventArgs e)//伪彩色处理
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("未打开图片！");
                return;
            }
            else if (bt3 == null)
            {
                MessageBox.Show("未进行灰度化！");
                return;
            }
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            int tGray = 0,r = 0,g = 0,b = 0;
            for(int i = 1; i < bt1.Width; i++)
            {
                for(int j = 1; j < bt1.Height; j++)
                {
                    c = bt1.GetPixel(i, j);
                    tGray = (int)(c.R * 0.114 + c.G * 0.587 + c.B * 0.299);
                    if(tGray >= 0 && tGray <= 63)
                    {
                        r = 0;
                        g = 254 - 4 * tGray;
                        b = 255;
                    }
                    if(tGray >=64 && tGray <= 127)
                    {
                        r = 0;
                        g = 4 * tGray - 254;
                        b = 510 - 4 * tGray;
                    }
                    if(tGray >=128 && tGray <= 191)
                    {
                        r = 4 * tGray - 510;
                        g = 255;
                        b = 0;
                    }
                    if(tGray >= 192 && tGray <= 255)
                    {
                        r = 255;
                        g = 1022 - 4 * tGray;
                        b = 0;
                    }
                    bt2.SetPixel(i,j,Color.FromArgb(r,g,b));
                }
                pictureBox2.Refresh();//刷新图片框
                pictureBox2.Image = bt2;
            }
        }
    }
}