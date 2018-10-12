using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{

    public class Game

    {
      

        //Extra features
        //The timer will be finished when I make the UI
        public void TheTimer()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //I'll eventually add whatever I want to meaure
            watch.Stop();
            var totalTime = watch.ElapsedMilliseconds;
        }
    }



}
