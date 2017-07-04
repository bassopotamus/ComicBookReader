using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComicBookReader
{


    public partial class ContentReader : Form
    {
        private Bitmap view;
        private List<Bitmap> comicToRead;
        private int pageNumber = 0;
        private string fileName;
        private double scaleBitmap = 0;
        //private int pressCount = 0;

        public ContentReader()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if ((Math.Abs(pictureBox1.Bottom) - this.Height) >= 0)
                {
                    pictureBox1.Top -= 20;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (Math.Abs(pictureBox1.Top) > 0)
                {
                    pictureBox1.Top += 20;
                }
            }
            else if (e.KeyCode == Keys.X)
            {
                Application.Exit();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                nextPage();
                GC.Collect();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                previousPage();
                GC.Collect();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openDialog.InitialDirectory = filePath;
            openDialog.Filter = "Comic Book Files (*.cbz)|*.cbz|Comic Book Files (*.cbr)|*.cbr";
            openDialog.RestoreDirectory = true;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = System.IO.Path.GetFullPath(openDialog.FileName);
                openComic(fileName);
            }
        }

        private Rectangle GetScreen()
        {
            return Screen.FromControl(this).Bounds;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextPage();
        }

        private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            previousPage();
        }

        private void nextPage()
        {
            pageNumber += 1;
            Task.Delay(100);
            if (pageNumber < comicToRead.Count)
            {
                view = comicToRead[pageNumber];
                pictureBox1.Width = GetScreen().Width;
                panel1.Width = GetScreen().Width;
                if (pictureBox1.Width > view.Width)
                {
                    scaleBitmap = (System.Convert.ToDouble(view.Width)) / (System.Convert.ToDouble(pictureBox1.Width));
                }
                else if (view.Width > pictureBox1.Width)
                {
                    scaleBitmap = (System.Convert.ToDouble(pictureBox1.Width)) / (System.Convert.ToDouble(view.Width));
                }
                Bitmap resizedView = new Bitmap(view, view.Width, (int)(view.Height * scaleBitmap));
                pictureBox1.Height = resizedView.Height;
                panel1.Height = resizedView.Height;
                pictureBox1.Top = 0;
                pictureBox1.Image = resizedView;
                ImageConverter converter = new ImageConverter();
                byte[] imgLength = (byte[])converter.ConvertTo(resizedView, typeof(byte[]));
                textBox1.Text = imgLength.Length.ToString();
                
            }
            else
            {
                pageNumber = (comicToRead.Count - 1);
            }

        }

        private void previousPage()
        {
            pageNumber -= 1;
            Task.Delay(100);
            if (pageNumber >= 0)
            {
                view = comicToRead[pageNumber];
                pictureBox1.Width = GetScreen().Width;
                panel1.Width = GetScreen().Width;
                if (pictureBox1.Width > view.Width)
                {
                    scaleBitmap = (System.Convert.ToDouble(view.Width)) / (System.Convert.ToDouble(pictureBox1.Width));
                }
                else if (view.Width > pictureBox1.Width)
                {
                    scaleBitmap = (System.Convert.ToDouble(pictureBox1.Width)) / (System.Convert.ToDouble(view.Width));
                }
                Bitmap resizedView = new Bitmap(view, view.Width, (int)(view.Height * scaleBitmap));
                pictureBox1.Height = resizedView.Height;
                panel1.Height = resizedView.Height;
                pictureBox1.Top = 0;
                pictureBox1.Image = resizedView;
                
            }
            else
            {
                pageNumber = 0;
            }

        }

        private void openComic(string fileName)
        {
            Comic comicBeingRead = new Comic(fileName);
            comicToRead = new List<Bitmap>();
            comicToRead = comicBeingRead.returnPage();
            readComic(comicToRead);
            GC.Collect();
        }

        private void readComic(List<Bitmap> comicToRead)
        {
            view = comicToRead[pageNumber];
            pictureBox1.Width = GetScreen().Width;
            panel1.Width = GetScreen().Width;
            if (pictureBox1.Width > view.Width)
            {
                scaleBitmap = (System.Convert.ToDouble(view.Width)) / (System.Convert.ToDouble(pictureBox1.Width));
            }
            else if (view.Width > pictureBox1.Width)
            {
                scaleBitmap = (System.Convert.ToDouble(pictureBox1.Width)) / (System.Convert.ToDouble(view.Width));
            }            
            Bitmap resizedView = new Bitmap(view, view.Width, (int)(view.Height * scaleBitmap));
            pictureBox1.Height = resizedView.Height;
            panel1.Height = resizedView.Height;
            pictureBox1.Image = resizedView;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.HorizontalScroll.Visible = false;
            panel1.VerticalScroll.Visible = false;


        }
    }
}
