using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Minesweeper;
using System.Media;

namespace Minesweeper
{

    public class Game
    {

    SoundPlayer player = new SoundPlayer("C:\\OffLimits.wav");
    SoundPlayer boom = new SoundPlayer("C:\\Bomb+2.wav");
    SoundPlayer cheers = new SoundPlayer("C:\\happykids.wav");




    //Extra features
    //The timer will be finished when I make the UI
    public String TheTimer()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            //I'll eventually add whatever I want to meaure
            watch.Stop();
            String totalTime = watch.ElapsedMilliseconds.ToString();
            return totalTime;

        }

   

    public void Music()
    {         
        player.PlayLooping();
      
    }

    public void StopMusic()
    {
      player.Stop();
    }

    public void Boom()
    {
      boom.Play();
    }

    public void Cheers()
    {
      cheers.Play();
    }


    }
}
