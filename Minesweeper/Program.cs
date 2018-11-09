using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Media;


namespace Minesweeper
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //move later on 
            using (SoundPlayer player = new SoundPlayer("C:\\OffLimits.wav"))
            {
                player.PlayLooping();
            }

            int mine = 0;
            bool flag = false;
            bool hide = true;

            int rowCount;
            Console.WriteLine("How many rows?: ");
            rowCount = Convert.ToInt32(Console.ReadLine());

            int columnCount;
            Console.WriteLine("How many columns?: ");
            columnCount = Convert.ToInt32(Console.ReadLine());

            int mineCount;
            Console.WriteLine("How many mines?: ");
            mineCount = Convert.ToInt32(Console.ReadLine());


            Block block = new Block(mine, flag, hide);
            
            Gridy grid = new Gridy(block, rowCount, columnCount);
            
            Console.WriteLine("\n A grid: \n \n"+ grid.ToString());
            
            grid.MineIt(mineCount); 

            Console.WriteLine("\n Mines: " + mineCount 
                +" \n Rows: " + rowCount
                + " \n Columns: " + columnCount
                + " \n" + grid.ToString());

            grid.NumberIt();
            Console.WriteLine("\n numbered grid: \n \n" + grid.ToString());

            Console.WriteLine("Press any key to close");
            Console.ReadLine();

        }

    }

}



