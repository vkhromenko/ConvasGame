using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace TheGameOfLife
{
    class Game
    {
        static Timer timer = new Timer();
        static List<Entity> collection = new List<Entity>();

        public static void Run()
        {
            
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            // Задание начальных условий
            StartingConditions(ref collection);
            timer.Enabled = true;
        }

        public static void Timer_Tick(object sender, EventArgs e)
        {
            RefreshForm(MainForm.buffer, collection);
            CheckAndReplace(ref collection);
        }
        /// <summary>
        /// Производит перерисовку формы в соответствии со значениями в collection
        /// </summary>
        /// <param name="mainFormGraphics"> элемент Graphics главной формы </param>
        /// <param name="collection"> коллекция "живых" клеток </param>

        private static void RefreshForm(BufferedGraphics mainFormGraphics, List<Entity> collection)
        {
            //перерисовка сетки окна
            PrintGrid.PrintGridFromForm(mainFormGraphics);
            foreach (Entity itemEntity in collection)
            {
                itemEntity.PrintEntity(mainFormGraphics);
            }
            mainFormGraphics.Render();
        }
        /// <summary>
        /// Задание начальных условий (случайная генерация).
        /// </summary>
        /// <param name="collection"> коллекция в которую помещаются клетки , координаты которых созданы с помощью генератора случайных чисел</param>
        private static void StartingConditions(ref List<Entity> collection)
        {
            Random randomize = new Random();
            HashSet<Entity> temp = new HashSet<Entity>();

            //temp.Add(new Entity(10, 10));
            //temp.Add(new Entity(11, 11));
            //temp.Add(new Entity(11, 12));
            //temp.Add(new Entity(10, 12));
            //temp.Add(new Entity(9, 12));

            for (int i = 0; i < 500; i++)
            {
                if (!temp.Add(new Entity(randomize.Next(MainForm.WIDTH), randomize.Next(MainForm.WIDTH))))
                {
                    i--;
                }
            }

            collection = temp.ToList();
        }


        /// <summary>
        /// Проверка условий:
        /// 1. На пустом поле, рядом с которым ровно 3 живые клетки, зарождается новая клетка;
        /// 2. Если у живой клетки есть 2 или 3 живые соседки, то эта клетка продолжает жить;
        /// 3/ Если соседей меньше 2 или больше 3, клетка умирает(от «одиночества» или от «перенаселённости»).
        /// </summary>
        /// <param name="collection"> Коллекция живых клеток</param>
        private static void CheckAndReplace(ref List<Entity> collection)
        {
            List<Entity> empty = new List<Entity>();
            List<Entity> nextGeneration = new List<Entity>();
            List<Entity> copyCollection = new List<Entity>(collection);

            #region Составление списка пустых клеток 
            for (int i = 0; i < MainForm.HEIGHT; i++)
            {
                for (int j = 0; j < MainForm.WIDTH; j++)
                {
                    Entity tempEntity = new Entity(j, i);
                    //если в листе с живыми клетками нет tempEntity, то добавить в лист с пустыми клетками
                    if (!collection.Contains(tempEntity))
                        empty.Add(tempEntity);
                }
            }
            #endregion

            List<Entity> storage = new List<Entity>();

            #region На пустом поле, рядом с которым ровно 3 живые клетки, зарождается новая клетка
            for (int count = 0; count < empty.Count; count++)
            {
                for (int xx = -1; xx <= 1; xx++)
                {
                    int XX = empty[count].posX + xx;

                    if (XX > MainForm.WIDTH - 1) XX = 0;
                    if (XX < 0) XX = MainForm.WIDTH - 1;

                    for (int yy = -1; yy <= 1; yy++)
                    {
                        int YY = empty[count].posY + yy;

                        if (YY > MainForm.HEIGHT - 1) YY = 0;
                        if (YY < 0) YY = MainForm.HEIGHT - 1;

                        Entity tempLife = new Entity(XX, YY);

                        if (collection.Contains(tempLife))
                        {
                            storage.Add(tempLife);
                        }
                    }
                }
                if (storage.Count == 3)
                {
                    nextGeneration.Add(empty[count]);
                }
                storage.Clear();
            }
            #endregion

            //RefreshForm(mainFormGraphics, collection);

            #region Проверка: Если соседей меньше 2 или больше 3, клетка умирает
            storage = new List<Entity>();
            Entity tempDie = null;
            for ( int count = 0; count < collection.Count; count++)
            {
                for (int xx = -1; xx <= 1; xx++)
                {
                    int XX = collection[count].posX + xx;

                    if (XX > MainForm.WIDTH - 1) XX = 0;
                    if (XX < 0) XX = MainForm.WIDTH - 1;
                    for (int yy = -1; yy <= 1; yy++)
                    { 
                        int YY = collection[count].posY + yy;

                        if (YY > MainForm.HEIGHT - 1) YY = 0;
                        if (YY < 0) YY = MainForm.HEIGHT - 1;

                        tempDie = new Entity(XX, YY);
                        if (collection[count].Equals(tempDie)) continue;

                        if (collection.Contains(tempDie))
                        {
                            storage.Add(tempDie);
                        }
                    }
                }
                if (storage.Count < 2 || storage.Count > 3)
                {
                    if(tempDie != null)
                    copyCollection.Remove(collection[count]);
                }
                storage.Clear();
            }
            #endregion

            nextGeneration.AddRange(copyCollection);
            collection = nextGeneration;
        }
    }
}
