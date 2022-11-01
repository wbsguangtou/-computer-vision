using System.Windows.Forms;

namespace xtxbh
{
    public partial class Form1 : Form
    {
        Bitmap bt1;
        Bitmap bt2;
        public Form1()
        {
            InitializeComponent();
        }

        private int getMax(int[] dt, int m)//��ȡ���ֵ
        {
            int max = dt[0];
            for (int i = 1; i < m; i++)
            {
                if (max < dt[i])

                {
                    max = dt[i];
                }
            }
            return max;
        }

        private int getMin(int[] dt, int m)//ȡ��Сֵ
        {
            int min = dt[0];
            for (int i = 1; i < m; i++)
            {
                if (min > dt[i])
                {
                    min = dt[i];
                }
            }
            return min;
        }
        private void fs()//��ֵ��ͼ��ʴ����
        {
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image);
            int R;
            for (int i = 1; i < bt1.Width - 1; i++)
            {
                for (int j = 1; j < bt1.Height - 1; j++)
                {
                    R = bt1.GetPixel(i, j).R;
                    if (R == 255)
                    {
                        bt2.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i - 1, j - 1, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i, j - 1, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i + 1, j - 1, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i - 1, j, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i + 1, j, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i - 1, j + 1, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i, j + 1, Color.FromArgb(255, 255, 255));
                        bt2.SetPixel(i + 1, j + 1, Color.FromArgb(255, 255, 255));
                    }
                }
                pictureBox2.Refresh();//ˢ��ͼƬ��
                pictureBox2.Image = bt2;
            }
        }

        public void pz()//��ֵ��ͼ�����ͺ���
        {
            bt1 = new Bitmap(pictureBox1.Image);
            bt2 = new Bitmap(pictureBox1.Image);
            int R1, R2, R3;
            for (int i = 0; i < bt1.Width - 1; i++)
            {
                for (int j = 0; j < bt1.Height - 1; j++)
            {
                    R1 = bt1.GetPixel(i, j).R;
                    if (R1 == 255)
                    {
                        R2 = bt1.GetPixel(i, j + 1).R;
                        R3 = bt1.GetPixel(i + 1, j).R;
                        if (R2 == 255 && R3 == 255)
                        {
                            bt2.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            bt2.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        }
                    }
                }
                pictureBox2.Refresh();//ˢ��ͼƬ��
                pictureBox2.Image = bt2;
            }
        }

        public void hdfs()
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(pictureBox1.Image);
            int[] gray = new int[5];
            for (int i = 1; i < bmp1.Width - 1; i++)
            {
                for (int j = 1; j < bmp1.Height - 1; j++)
                {
                    gray[0] = bmp1.GetPixel(i, j).R;
                    gray[1] = bmp1.GetPixel(i - 1, j).R;
                    gray[2] = bmp1.GetPixel(i + 1, j).R;
                    gray[3] = bmp1.GetPixel(i, j - 1).R;
                    gray[4] = bmp1.GetPixel(i, j + 1).R;
                    int min = Math.Abs(getMin(gray, 5) - gray[0]);
                    Color colorProcessed = Color.FromArgb(min, min, min);
                    bmp2.SetPixel(i, j, colorProcessed);
                }
                pictureBox2.Refresh();
                pictureBox2.Image = bmp2;
            }
        }

        public void hdpz()
        {
            Bitmap bmp1 = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(pictureBox1.Image);
            int[] gray = new int[20];
            for (int i = 1; i < bmp1.Width - 1; i++)
            {
                for (int j = 1; j < bmp1.Height - 1; j++)
                {
                    gray[0] = bmp1.GetPixel(i, j).R;
                    gray[1] = bmp1.GetPixel(i - 1, j).R;
                    gray[2] = bmp1.GetPixel(i + 1, j).R;
                    gray[3] = bmp1.GetPixel(i, j - 1).R;
                    gray[4] = bmp1.GetPixel(i, j + 1).R;
                    int max = getMax(gray, 5) + gray[0];
                    if (max > 255)
                    {
                        max = 255;
                    }
                    Color colorProcessed = Color.FromArgb(max, max, max);
                    bmp2.SetPixel(i, j, colorProcessed);
                }
                pictureBox2.Refresh();
                pictureBox2.Image = bmp2;
            }
        }
        private void button1_Click(object sender, EventArgs e)//��ֵͼ��ĸ�ʴ
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            fs();
        }

        private void button5_Click(object sender, EventArgs e)//��ͼƬ
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
            }
        }

        private void button6_Click(object sender, EventArgs e)//ͼ���ֵ��
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            Color p1;
            Bitmap btmp = new Bitmap(pictureBox1.Image);
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
     

        private void button7_Click(object sender, EventArgs e)//ͼ��ҶȻ�
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
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
        }

        private void button2_Click(object sender, EventArgs e)//��ֵ��ͼ������
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            pz();
        }

        private void button3_Click(object sender, EventArgs e)//��ֵ��ͼ����
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            fs();
            pz();
        }

        private void button4_Click(object sender, EventArgs e)//��ֵ��ͼ��պ�
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            pz();
            fs();
        }

        private void button8_Click(object sender, EventArgs e)//�Ҷ�ͼ��ʴ
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            hdfs();
        }

        private void button9_Click(object sender, EventArgs e)//�Ҷ�ͼ������
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            hdpz();
        }

        private void button10_Click(object sender, EventArgs e)//�Ҷ�ͼ����
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            hdfs();
            hdpz();
        }

        private void button11_Click(object sender, EventArgs e)//�Ҷ�ͼ��պ�
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("δ��ͼƬ��");
                return;
            }
            hdpz();
            hdfs();
        }
    }
}