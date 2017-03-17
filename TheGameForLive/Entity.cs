using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheGameOfLife
{
    class Entity : IEquatable<Entity>
    {
        public int posX { get; set; }
        public int posY { get; set; }

        public Entity(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void PrintEntity(BufferedGraphics mainFormGraphics)
        { 
            mainFormGraphics.Graphics.FillRectangle(Brushes.LightBlue, posX * MainForm.SCALE + 1, posY * MainForm.SCALE + 1, MainForm.SCALE - 1, MainForm.SCALE - 1);
        }

        public override bool Equals(object obj)
        {
            //If parameter is null return false
            if (obj == null)
            {
                return false;
            }

            //If parameter cannot be cast to Entity return false
            Entity temp = obj as Entity;
            if ((Object) temp == null)
            {
                return false;
            }

            //Return true if the fields match
            return (posX == temp.posX) && (posY == temp.posY);
        }

        public bool Equals(Entity other)
        { 
            return this.Equals((Object)other);
        }

        public override string ToString()
        {
            return string.Format("[x = {0}, y = {1}]", posX, posY);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
