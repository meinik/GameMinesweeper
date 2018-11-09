using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Minesweeper
{

  public class Gridy
  {
    private int MineNum { get; set; } = 10;// total number of mines
    private int Row { get; set; } = 10;// number of rows
    private int Column { get; set; } = 10; // number of columns
    private int Mines { get; set; } //values in the block
    private bool Flags { get; set; } = false;
    private bool Hiddens { get; set; } = true;


    //the array
    public Block[,] theGrid;

    public Gridy()
    {

    }

    //Constructor
    public Gridy(Block blocky, int row, int column)
    {
      Row = row;
      Column = column;

      theGrid = new Block[row, column];
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
          if (theGrid[i - 1, j].Mine == 9)
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
          if (theGrid[i + 1, j].Mine == 9)
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


    //3BV calculation 
    public int The3bv()
    {
      int i = 0;
      int j = 0;
      int count = 0;
      int count2 = 0;
      int the3bv = 0;
      //keep track of the checked cells
      bool[,] checkedCell = new bool[Row, Column];

      for (i = 0; i < Row; i++)
      {
        for (j = 0; j < Column; j++)
        {


          if (theGrid[i, j].Mine == 0)
          {
            if (checkedCell[i, j] != true)
            {
              count++;
            }

            if (i > 0 && j > 0 && theGrid[i - 1, j - 1].Mine != 9)
            {
              checkedCell[i - 1, j - 1] = true;
            }
            if (i > 0 && j >= 0 && theGrid[i - 1, j].Mine != 9)
            {
              checkedCell[i - 1, j] = true;
            }
            if (i >= 0 && j > 0 && theGrid[i, j - 1].Mine != 9)
            {
              checkedCell[i, j - 1] = true;
            }
            if (i < Row && j < Column - 1 && theGrid[i, j + 1].Mine != 9)
            {
              checkedCell[i, j + 1] = true;
            }
            if (i < Row - 1 && j < Column - 1 && theGrid[i + 1, j + 1].Mine != 9)
            {
              checkedCell[i + 1, j + 1] = true;
            }
            if (i < Row - 1 && j < Column && theGrid[i + 1, j].Mine != 9)
            {
              checkedCell[i + 1, j] = true;
            }
            if (i > 0 && j < Column - 1 && theGrid[i - 1, j + 1].Mine != 9)
            {
              checkedCell[i - 1, j + 1] = true;
            }
            if (i < Row - 1 && j > 0 && theGrid[i + 1, j - 1].Mine != 9)
            {
              checkedCell[i + 1, j - 1] = true;
            }

          }

          else if (theGrid[i, j].Mine < 9 && theGrid[i, j].Mine > 0) //no seee
          {

          }
        }
      }

      //count the not checked cells
      for (i = 0; i < Row; i++)
      {
        for (j = 0; j < Column; j++)
        {
          if (checkedCell[i, j] == false)
          {
            count2++;
          }
        }
      }
      Console.WriteLine("islas: " + count);
      Console.WriteLine("solitos: " + count2);

      //add the islands and the not checked cells
      the3bv = count + count2;
      return the3bv;


    }

  }

}