using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            int mineCount;
            int rowCount=0;
            int columnCount=0;

            Block block = new Block(mine, flag, hide);

            
            mineCount = 2;

            rowCount = 20;

            columnCount = 10;

            Console.WriteLine("Just one block: " + block);

            Grid grid = new Grid(block, rowCount, columnCount);
            
            Console.WriteLine("\n A grid: \n \n"+ grid.ToString());


            grid.MineIt(mineCount); //10 mines

            Console.WriteLine("\n Mines: " + mineCount 
                +" \n Rows: " + rowCount
                + " \n Columns: " + columnCount
                + " \n" + grid.ToString());

        }

    }

}



