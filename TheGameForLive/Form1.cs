using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGameOfLife
{
    public partial class MainForm : Form
    {
        public static int SCALE = 20;
        public static int WIDTH = 30;
        public static int HEIGHT = 30;

        public static Graphics mainFormGraphics;
        public static BufferedGraphicsContext context;
        public static BufferedGraphics buffer;

        public MainForm()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(WIDTH * SCALE, HEIGHT * SCALE);
            mainFormGraphics = CreateGraphics();

            context = new BufferedGraphicsContext();
            buffer = context.Allocate(mainFormGraphics, new Rectangle(0, 0, this.Width, this.Height));
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                PrintGrid.PrintGridFromForm(buffer);
                Game.Run();
            }
        }
    }
}
