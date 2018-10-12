using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;
namespace MinesweeperUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        //asserts that the new block has the assigned variables
        public void AssertBlockContent()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            Block block1 = new Block(mine, flag, hide);
            Assert.AreEqual(block1.Mine, 0);
            Assert.AreEqual(block1.Flag, false);
            Assert.AreEqual(block1.Hidden, true);
        }

        //asserts that a new grid is created and is not null
        public void CreateNewGrid()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            int rowCount = 5;
            int columnCount = 5;
           
            Block block1 = new Block(mine, flag, hide);
            Grid grid = new Grid(block1, rowCount, columnCount);
            Assert.IsNotNull(grid);
        }

        //asserts the flag variable can be set to the opposite value and back
        public void FlagUnflag()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;

            Block block1 = new Block(mine, flag, hide);
            block1.FlagIt();
            Assert.AreNotEqual(flag, block1.Flag);
            block1.FlagIt();
            Assert.AreEqual(flag, block1.Flag);
        }

        //asserts the Hidden value can be set from true to false and not back
        public void HideUnhide()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;

            Block block1 = new Block(mine, flag, hide);
            block1.UnhideIt();
            Assert.AreNotEqual(hide, block1.Hidden);
            block1.UnhideIt();
            Assert.IsFalse(block1.Hidden);
        }

        //creates two blocks and asserts they are different objects
        public void AssertTwoObjects()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            int row = 5;
            int column = 7;

            Block block1 = new Block(mine, flag, hide);
            Grid grid1 = new Grid(block1, row, column);
            Grid grid2 = new Grid(block1, row, column);
            Assert.AreNotSame(grid1, grid2);
        }


        //Creates a grid, selects a position and verifies the Mine value is not 9,
        // then mines the whole grid and verified the same position Mine value is 9
        public void AssertMines()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            int row = 2;
            int column = 2;
            int mines = 4;

            Block block1 = new Block(mine, flag, hide);
            Grid grid1 = new Grid(block1, row, column);

            Assert.AreNotEqual(9, grid1.theGrid[0, 0].Mine);

            grid1.MineIt(mines);
            Assert.AreEqual(9, grid1.theGrid[0, 0].Mine);

        }

        //creates a grid, adds 2 mines and verifies the adjacent cells have the values
        // that represent the adjacent mines count
        public void AssertAdjacentMineNumberIt()
        {
            int mine = 0;
            bool flag = false;
            bool hide = true;
            int row = 3;
            int column = 3;

            Block block1 = new Block(mine, flag, hide);
            Grid grid1 = new Grid(block1, row, column);

            grid1.theGrid[0, 0].Mine = 9;
            grid1.theGrid[1, 1].Mine = 9;

            grid1.NumberIt();

            Assert.AreEqual(grid1.theGrid[0, 1].Mine, 2);
            Assert.AreEqual(grid1.theGrid[1, 2].Mine, 1);
            Assert.AreEqual(grid1.theGrid[0, 3].Mine, 0);
        }
    }
}
