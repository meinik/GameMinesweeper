using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Block
    {

        private int mine = 0; //number 9  is a bomb, 0-8 is for hints
        private bool flag = false;
        private bool hidden = true;

        //constructor
        public Block(int mines, bool flags, bool hiddens)
        {
            this.Mine = mines;
            this.Flag = flags;
            this.Hidden = hiddens;
        }

        public bool UnhideIt () {
            // don't change because it can only be set to visible, not back
            return Hidden=false; 
        }
        
        public bool FlagIt ()
        {
            // this can be unflagged
            Flag = !Flag;
            return Flag;
        }

        //getter and setter
        public int Mine
        {
            get
            {
                return this.mine;
            }
            set
            {
                this.mine = value;
            
            }
        }

        //getter and setter
        public bool Flag
        {
            get
            {
                return this.flag;
            }
            set
            {
                this.flag = value;

            }
        }

        //getter and setter
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }
            set
            {
                this.hidden = value;
            }
        }

        // print 
        public override string ToString()
        {
            return Mine + " ";
        }


    }
}
