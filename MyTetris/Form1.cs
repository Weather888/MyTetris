using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTetris
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        // Размеры поля и размер клетки в пикселях
        public const int width = 15, height = 25, k = 15;
        // Массив для хранения падающей фигурки (для каждого блока 2 координаты [0, i] и [1, i]
        public int[,] shape = new int[2, 4];
        // Массив для хранения поля
        public int[,] field = new int[width, height];// какие квадратики заставлены
        public Bitmap bitfield = new Bitmap(k * (width + 1) + 1, k * (height + 3) + 1);//графическое полотно
        public Graphics gr; // Для совмещения Bitmap с PictureBox (рисования)
        public void SetShape()
        {
            Random x = new Random(DateTime.Now.Millisecond);
            switch (x.Next(7))
            { // Рандомно выбираем 1 из 7 возможных фигурок
                case 0: shape = new int[,] { { 2, 3, 4, 5 }, { 8, 8, 8, 8 } }; break;
                case 1: shape = new int[,] { { 2, 3, 2, 3 }, { 8, 8, 9, 9 } }; break;
                case 2: shape = new int[,] { { 2, 3, 4, 4 }, { 8, 8, 8, 9 } }; break;
                case 3: shape = new int[,] { { 2, 3, 4, 4 }, { 8, 8, 8, 7 } }; break;
                case 4: shape = new int[,] { { 3, 3, 4, 4 }, { 7, 8, 8, 9 } }; break;
                case 5: shape = new int[,] { { 3, 3, 4, 4 }, { 9, 8, 8, 7 } }; break;
                case 6: shape = new int[,] { { 3, 4, 4, 4 }, { 8, 7, 8, 9 } }; break;
            }
            TickTimer.Interval = 500;
        }
        public void FillField()
        {
            gr.Clear(Color.Black); //Очистим поле, полностью зарисовав его черным цветом
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (field[i, j] == 1)
                    { // Если клетка поля существует
                        gr.FillRectangle(Brushes.Yellow, i * k, j * k, k, k); // Рисуем в этом месте квадратик
                        gr.DrawRectangle(Pens.Blue, i * k, j * k, k, k);// границы квадратика
                    }
            for (int i = 0; i < 4; i++)
            { // Рисуем падающую фигуру
                gr.FillRectangle(Brushes.Pink, shape[1, i] * k, shape[0, i] * k, k, k);
                gr.DrawRectangle(Pens.Violet, shape[1, i] * k, shape[0, i] * k, k, k);//границы фигуры
            }
            FieldPictureBox.Image = bitfield;
        }
        public bool FindMistake()
        {
            for (int i = 0; i < 4; i++)
                if (shape[1, i] >= width || shape[0, i] >= height ||
                    shape[1, i] <= 0 || shape[0, i] <= 0 ||
                    field[shape[1, i], shape[0, i]] == 1)
                    return true;
            return false;
        }
        public Form1()
            {
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                // устанавливаем  размер формы 
                Width = 242;
                Height = 460;
                // задать картинку для полотна
                gr = Graphics.FromImage(bitfield);
            // расставить клетки по краям стакана
            for (int i = 0; i < width; i++)
                field[i, height - 1] = 1;
            for (int i = 0; i < height; i++)
            {
                field[0, i] = 1;
                field[width - 1, i] = 1;
            }
            // задать случайную фигуру
            SetShape();
            FillField();
            TickTimer.Start();
        }
        }
    }

