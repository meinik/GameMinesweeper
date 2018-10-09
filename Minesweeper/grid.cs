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
        private int Row { get; set; }  // number of rows
        private int Column { get; set; }  // number of columns
        private int Mines { get; set; } //values in the block
        private bool Flags { get; set; } = false;
        private bool Hiddens { get; set; } = true;


        //the array
        public Block[,] theGrid;
        
        //Constructor
        public Grid(Block blocky, int row, int column)
        {
            row = this.Row;
            column = this.Column;

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
                int row = random.Next(0, Row); //
                int col = random.Next(0, Column); //error
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

        //Extra feature
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

