using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Minesweeper
{

    class Grid
    {
        private int MineNum { get; set; } // total number of mines
        private int Row { get; set; } // number of rows
        private int Column { get; set; } // number of columns
        private int Mines { get; set; } //values in the block
        private bool Flags { get; set; } = false;
        private bool Hiddens { get; set; } = true;


        //the array
        public Block[,] theGrid;
        
        //Constructor
        public Grid(Block blocky, int row, int column)
        {
            Row = row;
            Column = column;

            theGrid = new Block[row,column];
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        theGrid[i, j] = new Block(Mines, Flags, Hiddens);
                    }
                }
        }

        // filling the field with mines
        public void MineIt(int counter)
        {
            MineNum = counter;
            var random = new Random();

            int placedMines = 0;
            while (placedMines < counter)
            {
                int row = random.Next(0, Row); 
                int col = random.Next(0, Column); 
                if (theGrid[row, col].Mine != 9)  
                {
                    theGrid[row, col].Mine = 9;
                    placedMines++;
                }
            }
        }

        // Overriding ToString to make it look like a grid 

        public override string ToString()
        {
            string space = "";
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    space += theGrid[i, j].ToString();
                }
                space += Environment.NewLine;
            }

            return space;
        }

        //counts adjacent mines
        public void CheckIt(int i, int j)
        {
            int count = 0;
            if (theGrid[i, j].Mine != 9)
            {
                if (i > 0 && j > 0)
                {
                    if (theGrid[i - 1, j - 1].Mine == 9)
                    {
                        count++;
                    }
                }
                if (i > 0 && j >= 0)
                {
                    if (theGrid[i - 1, j].Mine == 9) //not working
                    {
                        count++;
                    }
                }
                if (i >= 0 && j > 0)
                {
                    if (theGrid[i, j - 1].Mine == 9)
                    {
                        count++;
                    }
                }

                if (i < Row && j < Column - 1)
                {
                    if (theGrid[i, j + 1].Mine == 9)
                    {
                        count++;
                    }
                }
                if (i < Row - 1 && j < Column - 1)
                {
                    if (theGrid[i + 1, j + 1].Mine == 9)
                    {
                        count++;
                    }
                }
                if (i < Row - 1 && j < Column)
                {
                    if (theGrid[i + 1, j].Mine == 9) //not working
                    {
                        count++;
                    }
                }   
                if (i > 0 && j < Column - 1)
                {
                    if (theGrid[i - 1, j + 1].Mine == 9)
                    {
                        count++;
                    }
                }
                if (i < Row - 1 && j > 0)
                {
                    if (theGrid[i + 1, j - 1].Mine == 9)
                    {
                        count++;
                    }
                }
                theGrid[i, j].Mine = count;
            }
        }

        //uses the check it method to change the values of adjacent mines to each block
        public void NumberIt()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    CheckIt(i, j);   
                }
            }

        }
        //Extra features
        //The timer will be finished when I make the UI
        public void Timer()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

       //I'll eventually add whatever I want to meaure
            watch.Stop();
            var totalTime = watch.ElapsedMilliseconds;
        }

    }
}

