using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TheGameOfLife
{
    static class PrintGrid
    {
        /// <summary>
        /// Метод рисования сетки на главной форме
        /// </summary>
        /// <param name="mainFormGraphics"> Элемент Graphics главной формы</param>
        public static void PrintGridFromForm(BufferedGraphics mainFormGraphics)
        {
            //Заливка формы стандартным цветом
            mainFormGraphics.Graphics.Clear(Color.WhiteSmoke);
            //Рисование сетки
            Pen myPen = new Pen(Color.DarkGray);

            for (int xx = 0; xx < MainForm.WIDTH * MainForm.SCALE; xx += MainForm.SCALE)
            {
                mainFormGraphics.Graphics.DrawLine(myPen, xx, 0, xx, MainForm.HEIGHT * MainForm.SCALE);
            }
            for (int yy = 0; yy <= MainForm.HEIGHT * MainForm.SCALE; yy += MainForm.SCALE)
            {
                mainFormGraphics.Graphics.DrawLine(myPen, 0, yy, MainForm.WIDTH * MainForm.SCALE, yy);
            }

            myPen.Dispose();
        }

    }
}