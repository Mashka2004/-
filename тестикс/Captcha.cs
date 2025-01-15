using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace тестикс
{
    public partial class Captcha : Form
    {
        public Captcha()
        {
            InitializeComponent();
        }
        private string captchaText;
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void Captcha_Load(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }

        private void GenerateCaptcha()
        {
            captchaText = GenerateRandomCaptchaText(4);
            Bitmap captchaImage = GenerateCaptchaImage(captchaText);
            pictureBox1.Image = captchaImage;
        }

        private string GenerateRandomCaptchaText(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string text = "";

            for (int i = 0; i < length; i++)
            {
                text += chars[random.Next(chars.Length)];
            }
            return text;
        }

        private Bitmap GenerateCaptchaImage(string text)
        {
            const int width = 249;
            const int height = 91;
            Bitmap bmp = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.Clear(Color.White);
                Random random = new Random();

                for (int i = 0; i < 5; i++)
                {
                    Pen pen = new Pen(Color.LightGray, 1);
                    graphics.DrawLine(pen,
                        random.Next(0, width), random.Next(0, height),
                        random.Next(0, width), random.Next(0, height));
                }

                Font font = new Font("Arial", 20, FontStyle.Bold);
                int x = 10;
                for (int i = 0; i < text.Length; i++)
                {

                    int yOffset = random.Next(-5, 6); 

                    float rotationAngle = random.Next(-20, 21);


                    using (Matrix matrix = new Matrix())
                    {
                        matrix.RotateAt(rotationAngle, new PointF(x + 10, height / 2f)); 
                        graphics.Transform = matrix;

                        Brush brush = new SolidBrush(Color.Black);
                        graphics.DrawString(text[i].ToString(), font, brush, x, (height - font.Height) / 2 + yOffset);
                        x += 25;
                    }
                }
                graphics.ResetTransform();

                ApplyWaveDistortion(bmp, random);
                AddWhiteNoise(bmp, random, 30);
            }
            return bmp;
        }

        private void AddWhiteNoise(Bitmap bmp, Random random, int noiseLevel)
        {
            int width = bmp.Width;
            int height = bmp.Height;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (random.Next(0, 100) < noiseLevel)
                    {
                        int grayValue = random.Next(0, 256);
                        bmp.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                    }
                }
            }
        }

        private void ApplyWaveDistortion(Bitmap bmp, Random random)
        {
            const double amplitude = 2.0; 
            const double frequency = 0.1;

            int width = bmp.Width;
            int height = bmp.Height;

            Bitmap distortedBmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(distortedBmp))
            {
                g.Clear(Color.White);
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double offset = amplitude * Math.Sin(2 * Math.PI * x * frequency);
                    int newX = x + (int)offset;
                    if (newX >= 0 && newX < width)
                    {
                        distortedBmp.SetPixel(newX, y, bmp.GetPixel(x, y));
                    }
                }
            }

            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawImage(distortedBmp, 0, 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerateCaptcha();
        }
    }
}